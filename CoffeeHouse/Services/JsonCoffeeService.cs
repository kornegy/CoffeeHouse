using System.Text.Json;
using CoffeeHouse.Models;

namespace CoffeeHouse.Services
{
    public class JsonCoffeeService
    {
        private readonly string _path = Path.Combine("wwwroot", "data", "menu.json");

        public List<CoffeeItem> GetAll()
        {
            if (!File.Exists(_path))
            {
                return new List<CoffeeItem>();
            }
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<CoffeeItem>>(json) ?? new List<CoffeeItem>();
        }

        public void SaveAll(List<CoffeeItem> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }
    }
}
