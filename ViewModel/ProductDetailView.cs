using Mazada.Model;
using Mazada.Services;
using System;
using System.Collections.Generic;

namespace Mazada.ViewModel
{
    class ProductDetailView : ViewModelBase, INavigationAware<Product>
    {
        private Product _product;
        public RelayCommand DecreaseCommand => new RelayCommand(e => DecreaseQuantity(), e => Quantity > 1);
        public RelayCommand IncreaseCommand => new RelayCommand(e => IncreaseQuantity(), e => Product != null && Quantity <= Product.Stock);
        public RelayCommand BuyCommand => new RelayCommand(e => Buy());
        public RelayCommand AddToCartCommand => new RelayCommand(e => AddToCart());

        private INavigation _navigation;
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
            var orderItemNavArgs = new OrderItemNavArgs() { Product = Product, Quantity = Quantity };//Later the user id
            if (_navigation != null)
                _navigation.NavigateTo<OrderConfirmationViewModel, OrderItemNavArgs>(orderItemNavArgs);
            //StackNavigation.GetInstance().NavigateTo<OrderConfirmationViewModel>(orderItemNavArgs);
        }

        public void AddToCart()
        {
            Console.WriteLine("Added to cart +");
            var cartNavArgs = new CartNavArgs { Product = Product, Quantity = Quantity }; //Later the user id
        }

        public void OnNavigatedTo(INavigation navigation, Product parameter)
        {
            _navigation = navigation;
            Product = parameter;
        }
    }
}
