using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoffeeHouse.Models;
using System.Text.Json;

namespace CoffeeHouse.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        //main admin panel
        public IActionResult Index()
        {
            return View();
        }

        //menu's list(reading)
        [HttpGet]
        public async Task<IActionResult> MenuEditor()
        {
            var items = await GetMenuAsync();
            return View(items);
        }

        // add's form (show)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // add's logic (save)
        [HttpPost]
        public async Task<IActionResult> Create(CoffeeItem newItem)
        {
            var items = await GetMenuAsync();

            int newId = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            newItem.Id = newId;

            items.Add(newItem);
            await SaveMenuAsync(items); // saving in file

            return RedirectToAction("MenuEditor"); // back to list
        }

        // DELETE logic
        public async Task<IActionResult> Delete(int id)
        {
            var items = await GetMenuAsync();

            // find item by ID
            var itemToRemove = items.FirstOrDefault(x => x.Id == id);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                await SaveMenuAsync(items);
            }

            return RedirectToAction("MenuEditor");
        }

        // helpers methods to work with JSON

        private async Task<List<CoffeeItem>> GetMenuAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "menu.json");
            if (!System.IO.File.Exists(path)) return new List<CoffeeItem>();

            var json = await System.IO.File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<CoffeeItem>>(json) ?? new List<CoffeeItem>();
        }

        private async Task SaveMenuAsync(List<CoffeeItem> items)
        {
            var path = Path.Combine(_env.WebRootPath, "data", "menu.json");
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            await System.IO.File.WriteAllTextAsync(path, json);
        }
    }
}