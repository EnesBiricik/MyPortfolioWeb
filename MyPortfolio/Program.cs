using Hangfire;
using Microsoft.AspNetCore.Identity;
using MyPortfolio.BAL.DependencyResolvers.Microsoft;
using MyPortfolio.BAL.Helpers;
using MyPortfolio.BAL.Services;
using MyPortfolio.Controllers;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.UnitofWork;
using MyPortfolio.Entities.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddControllersWithViews();



#region Identity & Cookie

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<MainContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._������������";
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.MaxFailedAccessAttempts = 4;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(23); // oturum 23 saat
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "Enesbiricik"
    };
});

#endregion

//hangfire for statisic
#region statistic hangfire
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("Local"), new Hangfire.SqlServer.SqlServerStorageOptions
    {
        JobExpirationCheckInterval = TimeSpan.FromDays(7),
        DisableGlobalLocks = true
    });
});

builder.Services.AddHangfireServer();


#endregion

var profiles = ProfileHelper.GetProfiles();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

#region Hangfire Midleware for Statistics
app.UseHangfireServer();

var context = new MainContext();
var uow = new Uow(context);
var service = new StatisticService(uow);

var Seed = new SeedDataStatistic(service);
await Seed.Seed();

RecurringJob.AddOrUpdate(() => Seed.UpdateDates(), Cron.Daily, TimeZoneInfo.Local);
#endregion

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "panel",
        pattern: "panel",
        defaults: new { controller = "Dashboard", action = "Index" }
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
