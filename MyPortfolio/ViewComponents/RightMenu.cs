using Microsoft.AspNetCore.Mvc;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Web.ViewModel;

namespace MyPortfolio.Web.ViewComponents
{
    public class RightMenu : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly ISocialMediaService _socialMediaService;
        private readonly IBlogService _blogService;

        public RightMenu(ICategoryService categoryService, ISocialMediaService socialMediaService, IBlogService blogService)
        {
            _categoryService = categoryService;
            _socialMediaService = socialMediaService;
            _blogService = blogService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            var socialMedias = await _socialMediaService.GetAllAsync();
            var posts = await _blogService.GetAllAsyncForBlogs(true);

            var model = new RightMenuViewModel()
            {
                categories = categories.Data.Take(5).ToList(),
                socialMedias = socialMedias.Data,
                blogList = posts.Data
            };

            return View(model);
        }
    }
}
