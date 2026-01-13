using ShoppingList.Models;
using ShoppingList.Services;
using ShoppingList.Views;
using System;
using System.Collections.ObjectModel;

namespace ShoppingList.Views
{
    public partial class MainPage : ContentPage
    {
        private Models.ShoppingList shoppingList;
        private FileService fileService;

        public MainPage()
        {
            InitializeComponent();
            AddProductButton = this.FindByName<Button>("AddProductButton");
            AddProductButton.Clicked += OnAddProductClicked;
            fileService = new FileService();
            LoadData();
        }

        private async void LoadData()
        {
            shoppingList = await fileService.LoadShoppingList();
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            if (shoppingList is null)
            {
                return;
            }

            if (shoppingList.Products is null)
            {
                return;
            }

            for (int i = MainLayout.Children.Count - 1; i >= 2; i--)
            {
                MainLayout.Children.RemoveAt(i);
            }

            foreach (var product in shoppingList.Products)
            {
                var productView = new ProductView(product, shoppingList.Products);
                MainLayout.Children.Add(productView);
            }
        }

        private void OnAddProductClicked(object sender, EventArgs e)
        {
            if (shoppingList == null || shoppingList.Products == null)
                return;

            if (shoppingList.Products == null)
            {
                shoppingList.Products = new ObservableCollection<Product>();
            }

            var newProduct = new Product { Name = "Nowy produkt", Unit = "", Quantity = 1, IsPurchased = false };
            shoppingList.Products.Add(newProduct);
            DisplayProducts();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await fileService.SaveShoppingList(shoppingList);
        }
    }
}