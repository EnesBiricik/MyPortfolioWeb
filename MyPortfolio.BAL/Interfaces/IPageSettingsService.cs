using Microsoft.AspNetCore.Http;
using MyPortfolio.Common;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IPageSettingsService
    {
        Task<IResponse<PageSettingsUpdateDto>> UpdateAsync(PageSettingsUpdateDto dto, IFormFile? ProfilePhoto, IFormFile? TitleIcon, IFormFile? CommentAuthorProfilePhoto, IFormFile? NavbarLogoImage, IFormFile? FooterLogoImage);
        Task<IResponse<PageSettingsListDto>> Get<IDto>();
    }
}
