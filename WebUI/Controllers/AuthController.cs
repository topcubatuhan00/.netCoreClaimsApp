using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService? _authService;

        public AuthController(IAuthService? authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginAuthDto authDto)
        {
            var result = _authService.Login(authDto);
            if (result.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "batuhan topcu")
                };
                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var authProp = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProp).GetAwaiter().GetResult();
                return RedirectToAction("Index", "Home");   

            }
            TempData["Hata"] = result.Message;
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public IActionResult Register([FromForm] RegisterAuthDto authDto)
        {
            var result = _authService.Register(authDto);
            if (result.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "batuhan topcu")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProp = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProp).GetAwaiter().GetResult();
                return RedirectToAction("Index", "Home");
            }
            TempData["Hata"] = result.Message;
            return RedirectToAction("Register", "Auth");
        }

    }
}
