using Microsoft.Maui.Controls;
using ShoppingList.Models;
using System;
using System.Collections.ObjectModel;

namespace ShoppingList.Views
{
    public partial class ProductView : ContentView
    {
        private Product product;
        private ObservableCollection<Product> productsList;

        public ProductView(Product product, ObservableCollection<Product> productsList)
        {
            InitializeComponent();
            this.product = product;
            this.productsList = productsList;
            UpdateUI();
            DecreaseButton.Clicked += OnDecreaseClicked;
            IncreaseButton.Clicked += OnIncreaseClicked;
            QuantityEntry.TextChanged += OnQuantityTextChanged;
            PurchasedCheckBox.CheckedChanged += OnPurchasedChanged;
            DeleteButton.Clicked += OnDeleteClicked;
            NameEntry.TextChanged += OnNameTextChanged;
        }

        private void UpdateUI()
        {
            NameEntry.Text = product.Name;
            UnitLabel.Text = product.Unit;
            QuantityEntry.Text = product.Quantity.ToString();
            PurchasedCheckBox.IsChecked = product.IsPurchased;

            if (product.IsPurchased)
            {
                NameEntry.Opacity = 0.5;
                UnitLabel.Opacity = 0.5;
                QuantityEntry.Opacity = 0.5;
            }
            else
            {
                NameEntry.Opacity = 1;
                UnitLabel.Opacity = 1;
                QuantityEntry.Opacity = 1;
            }
        }

        private void OnDecreaseClicked(object sender, EventArgs e)
        {
            if (product.Quantity > 0)
            {
                product.Quantity--;
                UpdateUI();
            }
        }

        private void OnIncreaseClicked(object sender, EventArgs e)
        {
            product.Quantity++;
            UpdateUI();
        }

        private void OnQuantityTextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(e.NewTextValue, out int qty))
            {
                product.Quantity = qty;
            }
            else
            {
                product.Quantity = 0;
            }
            UpdateUI();
        }

        private void OnNameTextChanged(object sender, TextChangedEventArgs e)
        {
            product.Name = e.NewTextValue ?? string.Empty;
        }

        private void OnPurchasedChanged(object sender, CheckedChangedEventArgs e)
        {
            product.IsPurchased = e.Value;
            if (product.IsPurchased)
            {
                productsList.Remove(product);
                productsList.Add(product);
            }
            UpdateUI();
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (productsList != null)
            {
                productsList.Remove(product);
            }

            if (this.Parent is Layout parentLayout)
            {
                parentLayout.Children.Remove(this);
            }
            else
            {
                Element current = this.Parent;
                while (current != null)
                {
                    if (current is Layout layout)
                    {
                        layout.Children.Remove(this);
                        break;
                    }
                    current = current.Parent;
                }
            }
        }
    }
}