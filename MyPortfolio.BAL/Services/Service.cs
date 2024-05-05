using AutoMapper;
using FluentValidation;
using MyPortfolio.BAL.Extensions;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Services
{
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        public readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().CreateAsync(createdEntity);
                await _uow.SaveChanges();
                return new Response<CreateDto>(ResponseType.Success, dto);

            }
            return new Response<CreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<T>().FindAsync(dto.Id);
                if (unchangedData == null)
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} Id değerine sahip sonuç bulunamadı");
                var entity = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Update(entity, unchangedData);
                await _uow.SaveChanges();
                return new Response<UpdateDto>(ResponseType.Success, dto);

            }
            return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int Id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(Id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<uint> CountAsync()
        {
            var count = await _uow.GetRepository<T>().CountAsync();

            return count;
        }

    }
}
