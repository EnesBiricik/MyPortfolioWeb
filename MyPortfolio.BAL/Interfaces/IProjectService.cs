using Microsoft.AspNetCore.Http;
using MyPortfolio.Common;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IProjectService
    {
        Task<IResponse<ProjectCreateDto>> CreateAsync(ProjectCreateDto dto, IFormFile IconFile);
        Task<IResponse<ProjectUpdateDto>> UpdateAsync(ProjectUpdateDto dto, IFormFile? IconFile);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);

        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse<List<ProjectListDto>>> GetAllAsync();
        Task<uint> CountAsync();
    }
}