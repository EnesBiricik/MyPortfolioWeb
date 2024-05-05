using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogUpdateDto, Blog>().ReverseMap();
            CreateMap<BlogCreateDto, Blog>().ReverseMap();
            CreateMap<BlogListDto, Blog>().ReverseMap();
        }

    }
}
