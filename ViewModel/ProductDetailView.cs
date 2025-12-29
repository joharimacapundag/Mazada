using Mazada.Model;
using Mazada.Services;
using System;

namespace Mazada.ViewModel
{
    class ProductDetailView : ViewModelBase
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private Product _product;
        public Product Product
        {
            get =>  _product;
            set 
            { 
                _product = value;
                OnPropertyChanged();

            }
        }
        private int _quantity = 1;

        public int Quantity
        {
            get => _quantity;
            set 
            {
                _quantity = value;
                OnPropertyChanged();
                
            }
        }

        public RelayCommand DecreaseCommand => new RelayCommand(e => DecreaseQuantity(), e => Quantity > 1);
        public RelayCommand IncreaseCommand => new RelayCommand(e => IncreaseQuantity(), e => Product != null && Quantity <= Product.Stock);
        public RelayCommand BuyCommand => new RelayCommand(e => Buy());
        public RelayCommand AddToCartCommand => new RelayCommand(e => AddToCart());

        public override void OnParameterChanged(params object[] parameters)
        {
            if (parameters.Length == 1)
            {
                Product = (Product)parameters[0];
            }
        }

        public void DecreaseQuantity()
        {
            Quantity -= 1;
        }
        public void IncreaseQuantity()
        {
            Quantity += 1;
        }

        public void Buy()
        {
            Console.WriteLine("New Order +");
            Navigation.GetInstance().NavigateTo<OrderConfirmationViewModel>();
        }

        public void AddToCart()
        {
            Console.WriteLine("Added to cart +");
        }

       
    }
}
