using FinalYearProject.Services;
using FinalYearProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

            // Set up session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Bakes");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Bakes");
        }
    }
}