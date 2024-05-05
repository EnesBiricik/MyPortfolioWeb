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
    public class SocialMediaService : Service<SocialMediaCreateDto, SocialMediaUpdateDto, SocialMediaListDto, SocialMedia>, ISocialMediaService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<SocialMediaCreateDto> _validator;
        private readonly IValidator<SocialMediaUpdateDto> _updateValidator;

        public SocialMediaService(IMapper mapper, IValidator<SocialMediaCreateDto> createDtoValidator, IValidator<SocialMediaUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _mapper = mapper;
            _uow = uow;
            _validator = createDtoValidator;
            _updateValidator = updateDtoValidator;
        }

        public async Task<IResponse<SocialMediaCreateDto>> CreateAsync(SocialMediaCreateDto dto, IFormFile IconFile)
        {

            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var path = await FileHelper.CreateFile(IconFile);
                var createEntity = _mapper.Map<SocialMedia>(dto);
                createEntity.Icon = path;
                await _uow.GetRepository<SocialMedia>().CreateAsync(createEntity);
                await _uow.SaveChanges();
                return new Response<SocialMediaCreateDto>(ResponseType.Success, dto);
            }
            return new Response<SocialMediaCreateDto>(dto, result.ConvertToCustomValidationError());

        }
        public async Task<IResponse<List<SocialMediaListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<SocialMedia>().GetAllAsync();
            var dto = _mapper.Map<List<SocialMediaListDto>>(data);
            return new Response<List<SocialMediaListDto>>(ResponseType.Success, dto);

        }
        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {
            var data = await _uow.GetRepository<SocialMedia>().GetByFilterAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }
        public async Task<IResponse<SocialMediaUpdateDto>> UpdateAsync(SocialMediaUpdateDto dto, IFormFile? IconFile)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<SocialMedia>().FindAsync(dto.Id);
                if (unchangedData == null)
                    return new Response<SocialMediaUpdateDto>(ResponseType.NotFound, $"{dto.Id} Id değerine sahip sonuç bulunamadı");

                var entity = _mapper.Map<SocialMedia>(dto);
                if (IconFile != null)
                {
                    entity.Icon = await FileHelper.ReplaceFile(unchangedData.Icon, IconFile);
                }
                else
                    entity.Icon = unchangedData.Icon;
                //null değils eskisini alsın

                _uow.GetRepository<SocialMedia>().Update(entity, unchangedData);
                await _uow.SaveChanges();
                return new Response<SocialMediaUpdateDto>(ResponseType.Success, dto);
            }

            return new Response<SocialMediaUpdateDto>(ResponseType.ValidationError, dto);
        }
        public async Task<IResponse> RemoveAsync(int Id)
        {
            var data = await _uow.GetRepository<SocialMedia>().FindAsync(Id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<SocialMedia>().Remove(data);
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse> SocialMediaVisitCountUpdate(int id)
        {
            var socialMedia = await _uow.GetRepository<SocialMedia>().FindAsync(id);

            if (socialMedia == null)
                return new Response(ResponseType.NotFound, $"{id} ye sahip veri bulunamadı!");

            /*var entity = _mapper.Map<SocialMedia>(SocialMedia);
            entity.ClickCount=(uint)entity.ClickCount+1;*/
            socialMedia.ClickCount += 1;
            await _uow.SaveChanges();
            return new Response<IDto>(ResponseType.Success,"");
        }


    }
}
