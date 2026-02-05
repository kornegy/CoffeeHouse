using CoffeeHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CoffeeHouse.Controllers
{
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public CartController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var cartIds = GetCartIds(); // get list ID from session
            var allProducts = await GetMenuAsync();

            // filter
            // if ID is in list cartIds, adds in result
            var cartItems = new List<CoffeeItem>();
            foreach (var id in cartIds)
            {
                var product = allProducts.FirstOrDefault(p => p.Id == id);
                if (product != null) cartItems.Add(product);
            }

            return View(cartItems);
        }

        // add to cart
        [Authorize]
        public IActionResult Add(int id)
        {
            var cartIds = GetCartIds();
            cartIds.Add(id);
            SaveCart(cartIds); 

            return RedirectToAction("Menu", "Home");
        }

        // clear cart
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        // Reading list ID from session
        private List<int> GetCartIds()
        {
            var sessionData = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(sessionData)) return new List<int>();
            return JsonSerializer.Deserialize<List<int>>(sessionData) ?? new List<int>();
        }

        // Saving list ID in session
        private void SaveCart(List<int> ids)
        {
            var json = JsonSerializer.Serialize(ids);
            HttpContext.Session.SetString("Cart", json);
        }

        private async Task<List<CoffeeItem>> GetMenuAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "menu.json");
            if (!System.IO.File.Exists(path)) return new List<CoffeeItem>();
            var json = await System.IO.File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<CoffeeItem>>(json) ?? new List<CoffeeItem>();
        }
    }
}