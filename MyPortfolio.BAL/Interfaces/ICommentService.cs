using MyPortfolio.Common;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.Interfaces
{
    public interface ICommentService
    {
        Task<IResponse<CommentCreateDto>> CreateAsync(CommentCreateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);
        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse<List<CommentListDto>>> GetAllAsync();
        Task<IResponse> ShareStateUpdate(int Id);
    }
}
