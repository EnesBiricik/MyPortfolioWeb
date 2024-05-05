using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile()
        {
            CreateMap<ReplyCreateDto, Reply>().ReverseMap();
            CreateMap<ReplyListDto, Reply>().ReverseMap();
        }
    }
}
