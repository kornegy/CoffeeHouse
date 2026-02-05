using Microsoft.AspNetCore.Mvc;
using CoffeeHouse.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CoffeeHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env; // access to files

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Menu()
        {
            var items = await GetMenuAsync();
            return View(items);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Alternative method for reading file
        private async Task<List<CoffeeItem>> GetMenuAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "menu.json");

            if (!System.IO.File.Exists(path))
                return new List<CoffeeItem>();

            var json = await System.IO.File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<CoffeeItem>>(json) ?? new List<CoffeeItem>();
        }
    }
}