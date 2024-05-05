using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentListDto, Comment>().ReverseMap();
            CreateMap<CommentCreateDto, Comment>().ReverseMap();
        }
    }
}
