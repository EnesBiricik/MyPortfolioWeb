using AutoMapper;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Mappings.AutoMapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDto, Project>().ReverseMap();
            CreateMap<ProjectListDto, Project>().ReverseMap();
            CreateMap<ProjectUpdateDto, Project>().ReverseMap();
        }
    }
}
