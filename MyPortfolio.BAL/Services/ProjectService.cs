using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyPortfolio.BAL.Extensions;
using MyPortfolio.BAL.Helpers;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ProjectCreateDto> _validator;
        private readonly IValidator<ProjectUpdateDto> _updateValidator;

        public ProjectService(IUow uow, IMapper mapper, IValidator<ProjectCreateDto> validator, IValidator<ProjectUpdateDto> updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        public async Task<IResponse<ProjectCreateDto>> CreateAsync(ProjectCreateDto dto, IFormFile CoverPhoto)
        {
            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var path = await FileHelper.CreateFile(CoverPhoto);
                var createEntity = _mapper.Map<Project>(dto);
                createEntity.CoverPhoto = path;
                await _uow.GetRepository<Project>().CreateAsync(createEntity);
                await _uow.SaveChanges();
                return new Response<ProjectCreateDto>(ResponseType.Success, dto);
            }
            return new Response<ProjectCreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ProjectListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Project>().GetAllAsync();
            var dto = _mapper.Map<List<ProjectListDto>>(data);
            return new Response<List<ProjectListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {
            var data = await _uow.GetRepository<Project>().GetByFilterAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id}' ye ait veri bulunamadı!");
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int Id)
        {
            var data = await _uow.GetRepository<Project>().FindAsync(Id);
            if (data == null)
                return new Response<Project>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<Project>().Remove(data);
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<ProjectUpdateDto>> UpdateAsync(ProjectUpdateDto dto, IFormFile? IconFile)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<Project>().FindAsync(dto.Id);
                if (unchangedData == null)
                    return new Response<ProjectUpdateDto>(ResponseType.NotFound, $"{dto.Id} Id değerine sahip sonuç bulunamadı");

                var entity = _mapper.Map<Project>(dto);
                if (IconFile != null)
                {
                    entity.CoverPhoto = await FileHelper.ReplaceFile(unchangedData.CoverPhoto, IconFile);
                }
                else
                    entity.CoverPhoto = unchangedData.CoverPhoto;
                
                entity.Date = unchangedData.Date;

                _uow.GetRepository<Project>().Update(entity, unchangedData);
                await _uow.SaveChanges();
                return new Response<ProjectUpdateDto>(ResponseType.Success, dto);
            }

            return new Response<ProjectUpdateDto>(ResponseType.ValidationError, dto);
        }


        public async Task<uint> CountAsync()
        {
            var count = await _uow.GetRepository<Project>().CountAsync();

            return count;
        }
    }
}
