using System.Text.Json;
using CoffeeHouse.Models;

namespace CoffeeHouse.Services
{
    public class AuthService
    {
        private readonly string _path = Path.Combine("wwwroot", "data", "users.json");

        public List<UserRecord> GetAllUsers()
        {
            if (!File.Exists(_path))
                return new List<UserRecord>();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<UserRecord>>(json) ?? new List<UserRecord>();
        }

        public bool Validate(string username, string password)
        {
            var users = GetAllUsers();
            return users.Any(u => u.Username == username && u.Password == password);
        }
    }
}
