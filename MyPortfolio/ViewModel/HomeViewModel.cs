using MyPortfolio.Dtos;

namespace MyPortfolio.Web.ViewModel
{
    public class HomeViewModel
    {
        public PageSettingsListDto PageSettings { get; set; }
        public List<ProjectListDto>? Projects { get; set; }
        public List<BlogListDtoForBlogs> Blogs { get; set; }


    }
}
