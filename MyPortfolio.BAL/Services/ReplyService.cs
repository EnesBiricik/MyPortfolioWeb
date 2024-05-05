using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.BAL.Extensions;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ReplyCreateDto> _validator;
        public ReplyService(IUow uow, IMapper mapper, IValidator<ReplyCreateDto> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IResponse<ReplyCreateDto>> CreateAsync(ReplyCreateDto dto)
        {
            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var createEntity = _mapper.Map<Reply>(dto);
                await _uow.GetRepository<Reply>().CreateAsync(createEntity);
                try
                {
                    await _uow.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }

                return new Response<ReplyCreateDto>(ResponseType.Success, dto);
            }
            return new Response<ReplyCreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ReplyListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Reply>().GetQuery().Include(x => x.Comment).ToListAsync();
            var dto = _mapper.Map<List<ReplyListDto>>(data);
            return new Response<List<ReplyListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {
            var data = await _uow.GetRepository<Reply>().GetByFilterAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int Id)
        {
            var data = await _uow.GetRepository<Reply>().FindAsync(Id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<Reply>().Remove(data);
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse> ShareStateUpdate(int Id)
        {
            var reply = await _uow.GetRepository<Reply>().FindAsync(Id);
            if (reply == null)
                return new Response(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            reply.IsShared = !reply.IsShared;
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

    }
}
