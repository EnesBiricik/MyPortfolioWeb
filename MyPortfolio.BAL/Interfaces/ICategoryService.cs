using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Interfaces
{
    public interface ICategoryService : IService<CategoryCreateDto, CategoryUpdateDto, CategoryListDto, Category>
    {

    }
}
