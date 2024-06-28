using MyPortfolio.Dtos;

namespace MyPortfolio.Web.ViewModel
{
    public class RightMenuViewModel
    {
        public List<CategoryListDto> categories { get; set; }
        public List<SocialMediaListDto> socialMedias { get; set; }
        public List<BlogListDtoForBlogs> blogList { get; set; }

    }
}
