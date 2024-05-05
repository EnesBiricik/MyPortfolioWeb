using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.BAL.Mappings.AutoMapper;
using MyPortfolio.BAL.Services;
using MyPortfolio.BAL.ValidationRules.BlogValidators;
using MyPortfolio.BAL.ValidationRules.CategoryValidators;
using MyPortfolio.BAL.ValidationRules.CommentValidators;
using MyPortfolio.BAL.ValidationRules.ContactValidators;
using MyPortfolio.BAL.ValidationRules.PageSettingsValidators;
using MyPortfolio.BAL.ValidationRules.ProjectValidators;
using MyPortfolio.BAL.ValidationRules.ReplyValidators;
using MyPortfolio.BAL.ValidationRules.SocialMediaValidators;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.UnitofWork;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;

namespace MyPortfolio.BAL.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>();

            var mapperConfiguration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new BlogProfile());
                opt.AddProfile(new CategoryProfile());
                opt.AddProfile(new CommentProfile());
                opt.AddProfile(new ContactProfile());
                opt.AddProfile(new PageSettingsProfile());
                opt.AddProfile(new ProjectProfile());
                opt.AddProfile(new ReplyProfile());
                opt.AddProfile(new SocialMediaProfile());
            });

            #region Services
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            //services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPageSettingsService, PageSettingsService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IReplyService, ReplyService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<IStatisticService, StatisticService>();

            //services.AddTransient<IValidator<PageSettingsUpdateDto>, PageSettingsUpdateDtoValidator>();
            services.AddTransient<IValidator<BlogCreateDto>, BlogCreateDtoValidator>();
            services.AddTransient<IValidator<BlogUpdateDto>, BlogUpdateDtoValidator>();

            services.AddTransient<IValidator<CategoryCreateDto>, CategoryCreateDtoValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();

            services.AddTransient<IValidator<CommentCreateDto>, CommentCreateDtoValidator>();

            services.AddTransient<IValidator<ContactCreateDto>, ContactCreateDtoValidator>();

            services.AddTransient<IValidator<PageSettingsUpdateDto>, PageSettingsUpdateDtoValidator>();

            services.AddTransient<IValidator<ProjectCreateDto>, ProjectCreateDtoValidator>();
            services.AddTransient<IValidator<ProjectUpdateDto>, ProjectUpdateDtoValidator>();

            services.AddTransient<IValidator<ReplyCreateDto>, ReplyCreateDtoValidator>();

            services.AddTransient<IValidator<SocialMediaCreateDto>, SocialMediaCreateDtoValidator>();
            services.AddTransient<IValidator<SocialMediaUpdateDto>, SocialMediaUpdateDtoValidator>();

            #endregion


        }
    }
}
