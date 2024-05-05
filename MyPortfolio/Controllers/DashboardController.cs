using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.Dtos;
using MyPortfolio.Web.Extensions;
using MyPortfolio.Web.ViewModel;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISocialMediaService _socialMediaService;
        private readonly IProjectService _projectService;
        private readonly IContactService _contactService;
        private readonly IPageSettingsService _settingsService;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IReplyService _replyService;
        private readonly IStatisticService _statisticService;

        public DashboardController(ICategoryService categoryService, ISocialMediaService socialMediaService,
            IProjectService projectService, IContactService contactService, IPageSettingsService settingsService,
            IBlogService blogService, ICommentService commentService, IReplyService replyService, IStatisticService statisticService)
        {
            _categoryService = categoryService;
            _socialMediaService = socialMediaService;
            _projectService = projectService;
            _contactService = contactService;
            _settingsService = settingsService;
            _blogService = blogService;
            _commentService = commentService;
            _replyService = replyService;
            _statisticService = statisticService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _settingsService.Get<IDto>();
            var PostCount = await _blogService.CountAsync();
            var projectCount = await _projectService.CountAsync();
            var dailyvisitors = await _statisticService.GetAllAsync();
            var maxValue = (int)dailyvisitors.Data.OrderByDescending(x => x.value).First().value;
            var totalVisit =  settings.Data.VisitorCount;

            var model = new DashboardViewModel()
            {
                TotalPostCount = PostCount,
                TotalProjectCount = projectCount,
                SocialMediaVisitStatistic = new Dictionary<string, uint>(),
                DailyVisitors = new Dictionary<string, uint>(),
                maxVisitorValue = maxValue,
                TotalVisitorCount = totalVisit
            };

            foreach (var item in dailyvisitors.Data)
            {
                model.DailyVisitors.Add(item.date, item.value);
            }

            //SocialMedia Stats
            var socialMedias = await _socialMediaService.GetAllAsync();

            foreach (var item in socialMedias.Data)
            {
                model.SocialMediaVisitStatistic.Add(item.Name, item.ClickCount);
            }

            return View(model);
        }

        #region Blog

        public async Task<IActionResult> Blogs()
        {
            var blogs = await _blogService.GetAllAsync(false);
            return View(blogs.Data);
        }

        public async Task<IActionResult> BlogCreate()
        {
            var response = await _categoryService.GetAllAsync();
            if (response.Data.Count <= 0)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "Your Have Not Category. Please Add A Category!");
                return RedirectToAction("CategoryCreate");
            }

            ViewBag.Categories = response.Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlogCreate(BlogCreateDto dto, IFormFile CoverPhoto)
        {
            var response = await _categoryService.GetAllAsync();
            ViewBag.Categories = response.Data;

            if (CoverPhoto == null)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Cover Photo area is necessary");
                return View(dto);
            }

            if (dto.Name.Length > 50)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Name cannot be longer than 50 charachters.");
                return View(dto);
            }

            var result = await _blogService.CreateAsync(dto, CoverPhoto);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Blog is created!");
                return RedirectToAction("Blogs");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<BlogCreateDto>(result, "Blogs");
        }

        public async Task<IActionResult> BlogUpdate(int Id)
        {

            var response = await _categoryService.GetAllAsync();
            ViewBag.Categories = response.Data;

            var blog = await _blogService.GetByIdAsync<BlogUpdateDto>(Id);
            if (blog.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Blog is not found");
                return RedirectToAction("Blogs");
            }
            var dto = new BlogUpdateDto()
            {
                Id = blog.Data.Id,
                Name = blog.Data.Name,
                CoverPhoto = blog.Data.CoverPhoto,
                Description = blog.Data.Description,
                Content = blog.Data.Content,
                Date = blog.Data.Date,
                CategoryId = blog.Data.CategoryId,
                IsFeatured = blog.Data.IsFeatured
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> BlogUpdate(BlogUpdateDto dto, IFormFile CoverPhoto)
        {
            var result = await _blogService.UpdateAsync(dto, CoverPhoto);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Blog is updated!");
                return RedirectToAction("Blogs");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<BlogUpdateDto>(result, "Blogs");
        }

        public async Task<JsonResult> BlogDelete(int Id)
        {
            var response = await _blogService.RemoveAsync(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        public async Task<JsonResult> BlogFeatureStatus(int Id)
        {
            var response = await _blogService.FeatureStatusUpdate(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region Category

        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories.Data);
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryCreateDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Category is Created");
                return RedirectToAction("Categories");
            }
            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<CategoryCreateDto>(result, "Categories");
        }

        public async Task<IActionResult> CategoryUpdate(int id)
        {
            var category = await _categoryService.GetByIdAsync<CategoryUpdateDto>(id);
            if (category.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Category is not found");
                return RedirectToAction("Categories");
            }
            var dto = new CategoryUpdateDto()
            {
                Id = category.Data.Id,
                Name = category.Data.Name
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryUpdate(CategoryUpdateDto dto)
        {
            var result = await _categoryService.GetByIdAsync<CategoryUpdateDto>(dto.Id);
            if (result.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Category is not found");
                return View("Categories");
            }

            var updateResult = await _categoryService.UpdateAsync(dto);
            if (updateResult.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Category is updated!");
                return RedirectToAction("Categories");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<CategoryUpdateDto>(updateResult, "Categories");

        }

        public async Task<JsonResult> CategoryDelete(int id)
        {
            var response = await _categoryService.RemoveAsync(id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region Comment

        public async Task<IActionResult> Comments()
        {

            var comments = await _commentService.GetAllAsync();
            return View(comments.Data);
        }

        public async Task<JsonResult> CommentDelete(int Id)
        {

            var response = await _commentService.RemoveAsync(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        public async Task<JsonResult> CommentShareStateUpdate(int Id)
        {
            var response = await _commentService.ShareStateUpdate(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region Replies

        public async Task<IActionResult> Replies()
        {
            var replies = await _replyService.GetAllAsync();
            return View(replies.Data);
        }

        public async Task<JsonResult> ReplyDelete(int Id)
        {

            var response = await _replyService.RemoveAsync(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        public async Task<JsonResult> ReplyShareStateUpdate(int Id)
        {
            var response = await _replyService.ShareStateUpdate(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region SocialMedia

        public async Task<IActionResult> SocialMedias()
        {
            var socialMedias = await _socialMediaService.GetAllAsync();
            return View(socialMedias.Data);
        }

        public IActionResult SocialMediaCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SocialMediaCreate(SocialMediaCreateDto dto, IFormFile IconFile)
        {
            if (IconFile == null)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Icon Photo area is necessary");
                return View(dto);
            }

            var result = await _socialMediaService.CreateAsync(dto, IconFile);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Social Media is created!");
                return RedirectToAction("SocialMedias");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<SocialMediaCreateDto>(result, "SocialMedias");
        }

        public async Task<IActionResult> SocialMediaUpdate(int Id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync<SocialMediaUpdateDto>(Id);
            if (socialMedia.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Account is not found");
                return RedirectToAction("SocialMedias");
            }
            var dto = new SocialMediaUpdateDto()
            {
                Id = socialMedia.Data.Id,
                Name = socialMedia.Data.Name,
                Link = socialMedia.Data.Link,
                Icon = socialMedia.Data.Icon
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> SocialMediaUpdate(SocialMediaUpdateDto dto, IFormFile? IconFile)
        {
            var result = await _socialMediaService.UpdateAsync(dto, IconFile);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Social Media account is updated!");
                return RedirectToAction("SocialMedias");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<SocialMediaUpdateDto>(result, "SocialMedias");
        }

        public async Task<JsonResult> SocialMediaDelete(int id)
        {
            var response = await _socialMediaService.RemoveAsync(id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region Project

        public async Task<IActionResult> Projects()
        {
            var projects = await _projectService.GetAllAsync();
            return View(projects.Data);
        }

        public async Task<IActionResult> ProjectCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProjectCreate(ProjectCreateDto dto, IFormFile CoverPhoto)
        {
            if (CoverPhoto == null)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Cover Photo area is necessary");
                return View(dto);
            }

            var result = await _projectService.CreateAsync(dto, CoverPhoto);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Project is created!");
                return RedirectToAction("Projects");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<ProjectCreateDto>(result, "Projects");
        }

        public async Task<IActionResult> ProjectUpdate(int Id)
        {
            var project = await _projectService.GetByIdAsync<ProjectUpdateDto>(Id);
            if (project.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Project is not found");
                return RedirectToAction("Projects");
            }
            var dto = new ProjectUpdateDto()
            {
                Id = project.Data.Id,
                Name = project.Data.Name,
                CoverPhoto = project.Data.CoverPhoto,
                Description = project.Data.Description,
                GithubLink = project.Data.GithubLink,
                LiveDemoLink = project.Data.LiveDemoLink

            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> ProjectUpdate(ProjectUpdateDto dto, IFormFile? CoverPhoto)
        {
            var result = await _projectService.UpdateAsync(dto, CoverPhoto);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Project is updated!");
                return RedirectToAction("Projects");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<ProjectUpdateDto>(result, "Projects");
        }

        public async Task<JsonResult> ProjectDelete(int id)
        {
            var response = await _projectService.RemoveAsync(id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region Contact

        public async Task<IActionResult> Messages()
        {
            var messages = await _contactService.GetAllAsync();
            return View(messages.Data);
        }

        public async Task<JsonResult> MessageDelete(int Id)
        {
            var response = await _contactService.RemoveAsync(Id);
            if (response.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }


        public async Task<JsonResult> MessageRead(int id)
        {
            var comment = await _contactService.ReadStateUpdate(id);
            if (comment.ResponseType == ResponseType.Success)
                return Json(true);
            return Json(false);
        }

        #endregion

        #region PageSettings

        public async Task<IActionResult> PageSettings()
        {
            var settings = await _settingsService.Get<PageSettingsListDto>();
            return View(settings.Data);
        }

        public async Task<IActionResult> PageSettingsUpdate(int Id)
        {
            var project = await _settingsService.Get<PageSettingsListDto>();
            if (project.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Setting is not found");
                return RedirectToAction("PageSettings");
            }
            var dto = new PageSettingsUpdateDto()
            {
                Id = project.Data.Id,
                Name = project.Data.Name,
                ProfilePhoto = project.Data.ProfilePhoto,
                TitleIcon = project.Data.TitleIcon,
                CommentAuthorProfilePhoto = project.Data.CommentAuthorProfilePhoto,
                FooterLogoImage = project.Data.FooterLogoImage,
                NavbarLogoImage = project.Data.NavbarLogoImage,
                AboutDescription = project.Data.AboutDescription,
                Email = project.Data.Email,
                SloganSentence = project.Data.SloganSentence,
                Job = project.Data.Job,
                ShortDescription = project.Data.ShortDescription

            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> PageSettingsUpdate(PageSettingsUpdateDto dto, IFormFile? ProfilePhoto, IFormFile? TitleIcon, IFormFile? CommentAuthorProfilePhoto, IFormFile? NavbarLogoImage, IFormFile? FooterLogoImage)
        {
            var result = await _settingsService.UpdateAsync(dto, ProfilePhoto, TitleIcon, CommentAuthorProfilePhoto, NavbarLogoImage, FooterLogoImage);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Page settings is updated!");
                return RedirectToAction("PageSettings");
            }

            TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
            return this.ResponseRedirectAction<PageSettingsUpdateDto>(result, "PageSettings");
        }

        #endregion

    }
}
