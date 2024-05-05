using Microsoft.AspNetCore.Mvc;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Dtos;
using MyPortfolio.Web.ViewModel;

namespace MyPortfolio.Web.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly IPageSettingsService _pageSettingsService;

        public Navbar(IPageSettingsService pageSettingsService)
        {
            _pageSettingsService = pageSettingsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pageSettings = await _pageSettingsService.Get<PageSettingsListDto>();

            var model = new NavbarViewModel()
            {
                PageSettings = pageSettings.Data
            };
            return View(model);
        }
    }
}
