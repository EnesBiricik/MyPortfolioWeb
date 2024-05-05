using Microsoft.AspNetCore.Mvc;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Dtos;

namespace MyPortfolio.Web.ViewComponents
{
    public class Footer : ViewComponent
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly IPageSettingsService _pageSettingsService;

        public Footer(IPageSettingsService pageSettingsService, ISocialMediaService socialMediaService)
        {
            _pageSettingsService = pageSettingsService;
            _socialMediaService = socialMediaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socialMedias = await _socialMediaService.GetAllAsync();
            var pageSettings = await _pageSettingsService.Get<PageSettingsListDto>();

            var model = new FooterViewModel
            {
                SocialMediaList = socialMedias.Data,
                PageSettings = pageSettings.Data
            };

            return View(model);
        }

    }
}
