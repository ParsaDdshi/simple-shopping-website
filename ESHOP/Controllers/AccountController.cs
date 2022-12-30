using System.Security.Claims;
using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ESHOP.Controllers;

public class AccountController : Controller
{
    private IUserService _userService;
    public AccountController(IUserService userService) => _userService = userService;
        

    #region Register

    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterViewModel register)
    {
        if (!ModelState.IsValid)
            return View(register);

        var user = new User()
        {
            FullName = register.FullName,
            PhoneNumber = register.PhoneNumber,
            Email = register.Email.ToLower(),
            Password = register.Password,
            IsAdmin = false,
            RegisterTime = DateTime.Now
        };
        _userService.AddUser(user);
        _userService.Save();

        return View("SuccessRegister", register);
    }

    public IActionResult VerifyEmail(string email)
    {
        if (_userService.IsExistByEmail(email.ToLower()))
        {
            return Json("ایمیل وارد شده تکراری است");
        }
        return Json(true);
    }

        #endregion

    #region Login

    public IActionResult Login() => View();
    
    [HttpPost]
    public IActionResult Login(LoginViewModel login)
    {
        if (!ModelState.IsValid)
            return View(login);

        var user = _userService.GetUserForLogin(login.Email.ToLower(), login.Password);

        if (user == null)
        {
            ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نمیباشد");
            return View(login);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("IsAdmin", user.IsAdmin.ToString()),
            new Claim("FullName", user.FullName),
            new Claim("PhoneNumber", user.PhoneNumber),
            new Claim("FullName", user.FullName)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            IsPersistent = login.RememberMe
        };
        HttpContext.SignInAsync(principal, properties);
        return Redirect("/");
    }
    #endregion
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}