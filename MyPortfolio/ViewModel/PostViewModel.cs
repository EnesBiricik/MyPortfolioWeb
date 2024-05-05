using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;
using MyPortfolio.Web.Models;

namespace MyPortfolio.Web.ViewModel
{
    public class PostViewModel
    {
        public BlogListDto Blog { get; set; }
        public PageSettingsListDto PageSettings { get; set; }
    }
}
