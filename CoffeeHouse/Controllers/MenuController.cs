using Microsoft.AspNetCore.Mvc;
using CoffeeHouse.Models;
using CoffeeHouse.Services;
using CoffeeHouse.Filters;

namespace CoffeeHouse.Controllers
{
    public class MenuController : Controller
    {
        private readonly JsonCoffeeService _json;
        public MenuController(JsonCoffeeService json)
        {
            _json = json;
        }

        public IActionResult Index()
        {
            return View(_json.GetAll());
        }

        [HttpPost]
        [AdminAuthorize]
        public IActionResult Add(CoffeeItem item)
        {
            var list = _json.GetAll();
            item.Id = list.Count > 0 ? list.Max(x => x.Id) + 1 : 1;
            list.Add(item);
            _json.SaveAll(list);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AdminAuthorize]
        public IActionResult Delete(int id)
        {
            var list = _json.GetAll();
            var toRemove = list.FirstOrDefault(x => x.Id == id);
            if (toRemove != null)
            {
                list.Remove(toRemove);
                _json.SaveAll(list);
            }
            return RedirectToAction("Index");
        }
    }
}
