using MyPortfolio.Common;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto);
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);
        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse<List<ListDto>>> GetAllAsync();
        Task<uint> CountAsync();
    }
}
