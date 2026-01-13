using System.Collections.ObjectModel;

namespace ShoppingList.Models
{
    public class ShoppingList
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    }
}