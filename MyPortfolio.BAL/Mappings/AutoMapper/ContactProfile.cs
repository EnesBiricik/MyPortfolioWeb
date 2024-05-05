using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactCreateDto, Contact>().ReverseMap();
            CreateMap<ContactListDto, Contact>().ReverseMap();
        }
    }
}
