using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FinalYearProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFrontEnd loginFrontEnd)
        {
            if (!ModelState.IsValid)
            {
                return View(loginFrontEnd);
            }

            var user = await _userService.Login(loginFrontEnd.Email, loginFrontEnd.Password);
            
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(loginFrontEnd);
            }

            // Store user ID in session
            HttpContext.Session.SetInt32("UserId", user.Id);

            // Set up authentication and session
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role),
                new System.Security.Claims.Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            // Set up session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Bakes");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Bakes");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFrontEnd registerFrontEnd)
        {
            if (!ModelState.IsValid)
            {
                return View(registerFrontEnd);
            }

            var user = await _userService.Register(registerFrontEnd);
            
            if (user == null)
            {
                ModelState.AddModelError("", "Email already exists");
                return View(registerFrontEnd);
            }

            // Automatically log in the user after registration
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role),
                new System.Security.Claims.Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            // Set up session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Bakes");
        }
    }
}