using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ShoppingList.Models;

namespace ShoppingList.Services
{
    public class FileService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "shoppinglist.json");

        public async Task SaveShoppingList(Models.ShoppingList list)
        {
            var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<Models.ShoppingList> LoadShoppingList()
        {
            if (!File.Exists(filePath))
            {
                return new Models.ShoppingList();
            }
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Models.ShoppingList>(json) ?? new Models.ShoppingList();
        }
    }
}