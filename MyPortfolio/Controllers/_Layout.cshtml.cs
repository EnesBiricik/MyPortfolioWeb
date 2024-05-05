using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Dtos;

namespace MyPortfolio.Web.Controllers
{
    public class _Layout : Controller
    {
        public class LayoutModel : PageModel
        {
            private readonly IPageSettingsService _pageSettingsService;

            public PageSettingsListDto Settings { get; set; }

            public LayoutModel(IPageSettingsService pageSettingsService)
            {
                _pageSettingsService = pageSettingsService;
            }

            public async Task OnGetAsync()
            {
                var pageSettings = await _pageSettingsService.Get<PageSettingsListDto>();

                Settings = new PageSettingsListDto
                {
                    TitleIcon = pageSettings.Data.TitleIcon
                    // Diğer özellikleri de ayarlayabilirsiniz
                };
            }
        }
    }
}
