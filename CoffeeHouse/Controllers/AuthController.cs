using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CoffeeHouse.Models;
using System.Text.Json;

namespace CoffeeHouse.Controllers
{
    public class AuthController : Controller
    {
        // Declare a variable to access files
        private readonly IWebHostEnvironment _env;

        public AuthController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var path = Path.Combine(_env.WebRootPath, "data", "users.json");

            if (!System.IO.File.Exists(path))
                return Content("Error: file users.json has not found!");

            var json = await System.IO.File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try
            {
                var users = JsonSerializer.Deserialize<List<UserRecord>>(json, options);

                if (users == null) return Content("Users list is empty");

                var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    ViewBag.Error = "Wrong login or password";
                    return View();
                }

                string role = (user.Username == "admin") ? "Admin" : "User";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username ?? "Unknown"),
                    new Claim(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}