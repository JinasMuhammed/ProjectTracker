using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Application.Dtos;
using ProjectTracker.Application.Interfaces;

namespace ProjectTracker.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;
        public AccountController(IAuthService auth) => _auth = auth;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _auth.RegisterAsync(dto);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var token = await _auth.ValidateUserAndGenerateTokenAsync(dto);
            if (token == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(dto);
            }

            // Store token in cookie
            Response.Cookies.Append("X-Access-Token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });

            return RedirectToAction("Index", "Projects");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            return RedirectToAction(nameof(Login));
        }
    }
}
