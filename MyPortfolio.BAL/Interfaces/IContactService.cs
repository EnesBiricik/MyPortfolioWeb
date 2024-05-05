using MyPortfolio.Common;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IContactService
    {
        Task<IResponse<ContactCreateDto>> CreateAsync(ContactCreateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);
        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse<List<ContactListDto>>> GetAllAsync();
        Task<IResponse> ReadStateUpdate(int id);
    }
}
