using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.Entities.Concrete;
using MyPortfolio.Web.Extensions;
using MyPortfolio.Web.Models;

namespace MyPortfolio.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly MainContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(MainContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Sign-In-Up

        public IActionResult Login()
        {
            if (User.Identity?.Name != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] UserLoginModel user)
        {
            var foundUser = await _userManager.FindByNameAsync(user.UserName);

            if (foundUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(foundUser, user.Password, true, true);

                if (signInResult.Succeeded)
                {
                    return Json(true);
                }
                return Json("Login is Unsuccessful! Please Check Your User Name or Password!");

            }
            return Json("User Is Not Found");
        }

        public IActionResult Signup()
        {
            return View();
        }


       /* [HttpPost]
        public async Task<JsonResult> Signup([FromBody] UserSignupModel user)
        {

            if (ModelState.IsValid)
            {
                var foundUser = await _userManager.FindByNameAsync(user.UserName);
                if (foundUser != null)
                {
                    return Json("This Username Is Already Have!");
                }

                var foundUserEmail = await _userManager.FindByEmailAsync(user.Email);

                if (foundUserEmail != null)
                {
                    return Json("This Email Is Already Have!");
                }
                try
                {
                    var newUser = new AppUser { UserName = user.UserName, Email = user.Email };
                    var result = await _userManager.CreateAsync(newUser, user.Password);

                    if (result.Succeeded)
                    {
                        return Json(true);
                    }
                    return Json("User Can Not Created. " + result.Errors.First().Description);
                }
                catch (Exception e)
                {
                    return Json("Error! " + e.Message);
                }
            }
            return Json("User is Do Not Created!");
        }

        */


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["alerts"] = this.ViewAlert(AlertType.Info, $"Güvenli bir şekilde çıkış yaptınız ");
            return Redirect("/Home/Index");
        }
        #endregion

        #region User 

        [Authorize]
        public async Task<IActionResult> UserSettingsUpdate()
        {

            var user = await _userManager.FindByNameAsync(User.Identity!.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new UserUpdateModel()
            {
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserSettingsUpdate(UserUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.NewPassword)
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Error, "Old Password and New Password Can't Be The Same");
                    return RedirectToAction("UserSettingsUpdate", "Account");
                }

                var user = await _userManager.FindByNameAsync(User.Identity!.Name);

                if (user == null)
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Warning, "User Can't found");
                    return RedirectToAction("Index", "Dashboard");
                }

                var response = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!response)
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Password is Wrong!");
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }

                if (model.NewPassword != null)
                {
                    var resultPassword = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

                    if (!resultPassword.Succeeded)
                    {
                        TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Error! " + resultPassword.Errors.First());
                        return RedirectToAction("UserSettingsUpdate", "Account");
                    }
                }

                user.Email = model.Email;
                user.UserName = model.UserName;

                var updateResponse = await _userManager.UpdateAsync(user);

                if (!updateResponse.Succeeded)
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Error! " + updateResponse.Errors.First());
                    return RedirectToAction("UserSettingsUpdate", "Account");
                }
                else
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Success, "User Bilgileri Başarıyla Güncellendi:)");
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Dashboard");
                }

            }
            TempData["alerts"] = this.ViewAlert(AlertType.Info, "Something went wrong");
            return RedirectToAction("UserSettingsUpdate", "Account");
        }
        #endregion       
    }
}
