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
    public class ContactService : IContactService
    {

        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ContactCreateDto> _validator;

        public ContactService(IUow uow, IMapper mapper, IValidator<ContactCreateDto> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IResponse<ContactCreateDto>> CreateAsync(ContactCreateDto dto)
        {

            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var createEntity = _mapper.Map<Contact>(dto);
                await _uow.GetRepository<Contact>().CreateAsync(createEntity);
                await _uow.SaveChanges();
                return new Response<ContactCreateDto>(ResponseType.Success, "");
            }
            return new Response<ContactCreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ContactListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Contact>().GetAllAsync();
            var dto = _mapper.Map<List<ContactListDto>>(data);
            return new Response<List<ContactListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {
            var data = await _uow.GetRepository<Contact>().GetByFilterAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id}' ye ait veri bulunamadı!");
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int Id)
        {
            var data = await _uow.GetRepository<Contact>().FindAsync(Id);
            if (data == null)
                return new Response<Contact>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<Contact>().Remove(data);

            return new Response(ResponseType.Success);
        }

        public async Task<IResponse> ReadStateUpdate(int id)
        {
            var message = await _uow.GetRepository<Contact>().FindAsync(id);

            if (message == null)
                return new Response(ResponseType.NotFound, $"{id} ye sahip veri bulunamadı!");

            message.IsRead = !message.IsRead;
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);

        }
    }
}
