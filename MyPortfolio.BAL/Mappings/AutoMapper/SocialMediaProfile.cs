using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class SocialMediaProfile : Profile
    {
        public SocialMediaProfile()
        {
            CreateMap<SocialMediaCreateDto, SocialMedia>().ReverseMap();
            CreateMap<SocialMediaListDto, SocialMedia>().ReverseMap();
            CreateMap<SocialMediaUpdateDto, SocialMedia>().ReverseMap();
        }
    }
}
