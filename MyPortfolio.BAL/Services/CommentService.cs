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
    public class CommentService : ICommentService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CommentCreateDto> _validator;
        public CommentService(IUow uow, IMapper mapper, IValidator<CommentCreateDto> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IResponse<CommentCreateDto>> CreateAsync(CommentCreateDto dto)
        {
            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var createEntity = _mapper.Map<Comment>(dto);
                await _uow.GetRepository<Comment>().CreateAsync(createEntity);
                await _uow.SaveChanges();
                return new Response<CommentCreateDto>(ResponseType.Success, dto);
            }
            return new Response<CommentCreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<CommentListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Comment>().GetQuery().Include(x => x.Replies.Where(x => x.IsShared == true)).ToListAsync();
            var dto = _mapper.Map<List<CommentListDto>>(data);
            return new Response<List<CommentListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {

            var data = await _uow.GetRepository<Comment>().GetByFilterAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int Id)
        {

            var data = await _uow.GetRepository<Comment>().FindAsync(Id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<Comment>().Remove(data);
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse> ShareStateUpdate(int Id)
        {
            var comment = await _uow.GetRepository<Comment>().FindAsync(Id);
            if (comment == null)
                return new Response(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            comment.IsShared = !comment.IsShared;
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }
    }
}
