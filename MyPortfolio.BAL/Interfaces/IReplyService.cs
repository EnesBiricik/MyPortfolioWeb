using MyPortfolio.Common;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IReplyService
    {
        Task<IResponse<ReplyCreateDto>> CreateAsync(ReplyCreateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);
        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse<List<ReplyListDto>>> GetAllAsync();
        Task<IResponse> ShareStateUpdate(int Id);
    }
}
