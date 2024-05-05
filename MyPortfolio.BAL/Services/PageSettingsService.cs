
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyPortfolio.BAL.Helpers;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Services
{
    public class PageSettingsService : IPageSettingsService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<PageSettingsUpdateDto> _validator;

        public PageSettingsService(IUow uow, IMapper mapper, IValidator<PageSettingsUpdateDto> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IResponse<PageSettingsListDto>> Get<IDto>()
        {
            var data = _uow.GetRepository<PageSettings>().GetQuery().FirstOrDefault();
            if (data == null)
                return new Response<PageSettingsListDto>(ResponseType.NotFound, $"{data.Id}' ye ait veri bulunamadı!");

            var dto = _mapper.Map<PageSettingsListDto>(data);
            return new Response<PageSettingsListDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse<PageSettingsUpdateDto>> UpdateAsync(PageSettingsUpdateDto dto, IFormFile? ProfilePhoto, IFormFile? TitleIcon, IFormFile? CommentAuthorProfilePhoto, IFormFile? NavbarLogoImage, IFormFile? FooterLogoImage)
        {
            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<PageSettings>().FindAsync(dto.Id);
                if (unchangedData == null)
                    return new Response<PageSettingsUpdateDto>(ResponseType.NotFound, $"{dto.Id} Id değerine sahip sonuç bulunamadı");
                var entity = _mapper.Map<PageSettings>(dto);

                if (ProfilePhoto != null)
                {
                    if (unchangedData.ProfilePhoto == "profile.png")
                    {
                        var path = await FileHelper.CreateFile(ProfilePhoto);
                        entity.ProfilePhoto = path;
                    }
                    else
                    {
                        var path = await FileHelper.ReplaceFile(unchangedData.ProfilePhoto, ProfilePhoto);
                        entity.ProfilePhoto = path;
                    }

                }
                else
                    entity.ProfilePhoto = unchangedData.ProfilePhoto;

                if (TitleIcon != null)
                {
                    if (unchangedData.TitleIcon == "TitleIcon.png")
                    {
                        var path = await FileHelper.CreateFile(TitleIcon);
                        entity.TitleIcon = path;
                    }
                    else
                    {
                        var path = await FileHelper.ReplaceFile(unchangedData.TitleIcon, TitleIcon);
                        entity.TitleIcon = path;
                    }

                }
                else
                    entity.TitleIcon = unchangedData.TitleIcon;

                if (CommentAuthorProfilePhoto != null)
                {
                    if (unchangedData.CommentAuthorProfilePhoto == "CommentAuthorProfilePhoto.png")
                    {
                        var path = await FileHelper.CreateFile(CommentAuthorProfilePhoto);
                        entity.CommentAuthorProfilePhoto = path;
                    }
                    else
                    {
                        var path = await FileHelper.ReplaceFile(unchangedData.CommentAuthorProfilePhoto, CommentAuthorProfilePhoto);
                        entity.CommentAuthorProfilePhoto = path;
                    }

                }
                else
                    entity.CommentAuthorProfilePhoto = unchangedData.CommentAuthorProfilePhoto;

                if (NavbarLogoImage != null)
                {
                    if (unchangedData.NavbarLogoImage == "profile.png")
                    {
                        var path = await FileHelper.CreateFile(NavbarLogoImage);
                        entity.NavbarLogoImage = path;
                    }
                    else
                    {
                        var path = await FileHelper.ReplaceFile(unchangedData.NavbarLogoImage, NavbarLogoImage);
                        entity.NavbarLogoImage = path;
                    }

                }
                else
                    entity.NavbarLogoImage = unchangedData.NavbarLogoImage;

                if (FooterLogoImage != null)
                {
                    if (unchangedData.FooterLogoImage == "profile.png")
                    {
                        var path = await FileHelper.CreateFile(FooterLogoImage);
                        entity.FooterLogoImage = path;
                    }
                    else
                    {
                        var path = await FileHelper.ReplaceFile(unchangedData.FooterLogoImage, FooterLogoImage);
                        entity.FooterLogoImage = path;
                    }

                }
                else
                    entity.FooterLogoImage = unchangedData.FooterLogoImage;

                _uow.GetRepository<PageSettings>().Update(entity, unchangedData);
                await _uow.SaveChanges();
                return new Response<PageSettingsUpdateDto>(ResponseType.Success, dto);
            }

            return new Response<PageSettingsUpdateDto>(ResponseType.ValidationError, dto);
        }
    }
}
