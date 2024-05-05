using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class PageSettingsProfile : Profile
    {
        public PageSettingsProfile()
        {
            CreateMap<PageSettingsListDto, PageSettings>().ReverseMap();
            CreateMap<PageSettingsUpdateDto, PageSettings>().ReverseMap();
        }
    }
}
