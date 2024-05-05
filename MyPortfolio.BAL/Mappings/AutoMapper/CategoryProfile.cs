using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryListDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }
    }
}
