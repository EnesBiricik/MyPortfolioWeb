using Microsoft.AspNetCore.Http;
using MyPortfolio.Common;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.Interfaces
{
    public interface ISocialMediaService
    {
        //Task Create(T Entity);

        Task<IResponse<SocialMediaCreateDto>> CreateAsync(SocialMediaCreateDto dto, IFormFile IconFile);

        Task<IResponse<List<SocialMediaListDto>>> GetAllAsync();
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);

        Task<IResponse<SocialMediaUpdateDto>> UpdateAsync(SocialMediaUpdateDto dto, IFormFile? IconFile);

        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse> SocialMediaVisitCountUpdate(int id);



    }
}
