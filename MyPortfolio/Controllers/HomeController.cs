using Microsoft.AspNetCore.Mvc;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;
using MyPortfolio.Web.Extensions;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.ViewModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Primitives;

namespace MyPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageSettingsService _pageSettingsService;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IContactService _contactService;
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;
        private readonly IReplyService _replyService;
        private readonly ISocialMediaService _socialMediaService;

        public HomeController(IPageSettingsService pageSettingsService, IBlogService blogService, IContactService contactService,
            ICategoryService categoryService, IProjectService projectService, ICommentService commentService, IReplyService replyService, ISocialMediaService socialMediaService)
        {
            _pageSettingsService = pageSettingsService;
            _blogService = blogService;
            _contactService = contactService;
            _categoryService = categoryService;
            _projectService = projectService;
            _commentService = commentService;
            _replyService = replyService;
            _socialMediaService = socialMediaService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsyncForBlogs();
            var projects = await _projectService.GetAllAsync();
            var pageSettings = await _pageSettingsService.Get<PageSettingsListDto>();

            var model = new HomeViewModel()
            {
                Blogs = blogs.Data.Take(5).ToList(),
                Projects = projects.Data,
                PageSettings = pageSettings.Data
            };
            return View(model);
        }

        public async Task<IActionResult> About()
        {
            var about = await _pageSettingsService.Get<IDto>();
            return View(about.Data);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactCreateModel model)
        {

            var form = Request.Form;
            Request.Form.TryGetValue("g-recaptcha-response", out StringValues value);

            if (string.IsNullOrEmpty(value))
            {
                ModelState.AddModelError("reCaptcha", "Google Doğrulama İşlemi Gerçekleşmedi");
                return View(model);
            }

            var dto = new ContactCreateDto
            {
                EmailAddress = model.EmailAddress,
                IsRead = model.IsRead,
                Message = model.Message,
                Name = model.Name,
                Subject = model.Subject
            };

            var result = await _contactService.CreateAsync(dto);
            if (result.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Success, "Mesajınız Gönderildi.");
                return RedirectToAction("Index");
            }
            TempData["alerts"] = this.ViewAlert(AlertType.Error, "İşlem Başarısız.");
            return this.ResponseRedirectAction<ContactCreateDto>(result, "Contact");
        }

        public async Task<IActionResult> Post(int id)
        {
            var post = await _blogService.GetByIdAsync<BlogListDto>(id);
            var pageSettings = await _pageSettingsService.Get<PageSettingsListDto>();


            if (post.ResponseType == ResponseType.NotFound)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "This Post Is Not Found!");
                return RedirectToAction("Index");
            }

            var model = new PostViewModel
            {
                Blog = post.Data,
                PageSettings = pageSettings.Data
            };

            return View(model);
        }

        public async Task<IActionResult> Blogs()
        {

            var settings = await _pageSettingsService.Get<PageSettingsListDto>();

            ViewBag.Settings = settings.Data;

            var posts = await _blogService.GetAllAsyncForBlogs(false);
            return View(posts.Data);
        }

        public async Task<IActionResult> LoadMore([FromQuery] int pageIndex)
        {

            var settings = await _pageSettingsService.Get<PageSettingsListDto>();

            ViewBag.Settings = settings.Data;

            var posts = await _blogService.LoadMore(pageIndex);
            return View(posts.Data);
        }

        public async Task<IActionResult> ProjectDetail(int id)
        {
            var settings = await _pageSettingsService.Get<PageSettingsListDto>();

            ViewBag.Settings = settings.Data;


            var project = await _projectService.GetByIdAsync<ProjectListDto>(id);
            return View(project.Data);
        }

        public async Task<IActionResult> Project()
        {
            var projects = await _projectService.GetAllAsync();
            return View(projects.Data);
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories.Data);
        }

        public async Task<IActionResult> Category(int id)
        {

            var category = await _categoryService.GetByIdAsync<CategoryListDto>(id);

            ViewBag.CategoryName = category.Data.Name;


            var categories = await _blogService.GetAllByCategoryIdAsync(id);
            var pageSettings = await _pageSettingsService.Get<PageSettingsListDto>();

            var model = new HomeViewModel()
            {
                Blogs = categories.Data,
                PageSettings = pageSettings.Data
            };
            return View(model);
        }


        [HttpPost]
        public async Task<JsonResult> CommentCreate([FromBody] CommentCreateModel commentCreateModel)
        {
            if (ModelState.IsValid)
            {
                CommentCreateDto commentCreateDto = new CommentCreateDto()
                {
                    AuthorEmailAddress = commentCreateModel.AuthorEmailAddress,
                    AuthorName = commentCreateModel.AuthorName,
                    BlogId = commentCreateModel.BlogId,
                    CommentText = commentCreateModel.CommentText,
                    IsShared = false
                };

                var comment = await _commentService.CreateAsync(commentCreateDto);

                if (comment.ResponseType == ResponseType.Success)
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Success, "Your comment is sent. It will be published after review");
                    return Json(true);
                }

                TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
                return Json(false);
            }

            return Json(false);
        }

        [HttpPost]
        public async Task<JsonResult> ReplyCreate([FromBody] ReplyCreateModel replyCreateModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new ReplyCreateDto()
                {
                    AuthorName = replyCreateModel.AuthorName,
                    AuthorEmailAddress = replyCreateModel.AuthorEmailAddress,
                    CommentId = replyCreateModel.CommentId,
                    CommentText = replyCreateModel.CommentText,
                    IsShared = false
                };

                var response = await _replyService.CreateAsync(dto);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Success, "Your comment is sent. It will be published after review");
                    return Json(true);
                }


                TempData["alerts"] = this.ViewAlert(AlertType.Error, "Action is failed");
                return Json(false);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<JsonResult> SocialMediaClickCount([FromBody] int id)
        {
            var response = await _socialMediaService.SocialMediaVisitCountUpdate(id);
            if (response.ResponseType == ResponseType.Success)
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<JsonResult> IncreaseVisitor([FromServices] IStatisticService service)
        {
            await service.IncreaseVisitor();
            return Json(true);
        }

    }
}