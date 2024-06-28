using Microsoft.AspNetCore.Http;
using MyPortfolio.Common;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IBlogService : IService<BlogCreateDto, BlogUpdateDto, BlogListDto, Blog>
    {
        Task<IResponse<BlogCreateDto>> CreateAsync(BlogCreateDto dto, IFormFile CoverPhoto);
        Task<IResponse<List<BlogListDto>>> GetAllAsync(bool isFeatured = false);

        Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id);
        Task<IResponse> RemoveAsync(int Id);
        Task<IResponse<BlogUpdateDto>> UpdateAsync(BlogUpdateDto dto, IFormFile? IconFile);
        Task<IResponse> FeatureStatusUpdate(int Id);
        Task<IResponse<List<BlogListDtoForBlogs>>> GetAllByCategoryIdAsync(int id);
        Task<IResponse<List<BlogListDtoForBlogs>>> LoadMore(int pageIndex);
        Task<IResponse<List<BlogListDtoForBlogs>>> GetAllAsyncForBlogs(bool isF = false);

    }
}
