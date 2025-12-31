using Mazada.Model;
using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mazada.ViewModel
{
    class OrderConfirmationViewModel : ViewModelBase, INavigationAware<OrderItemNavArgs>
    {
        private Product _product;
        public Product Product
        {
            get => _product; 
            set 
            { 
                _product = value;
                OnPropertyChanged();
            }
        }
        private int _quantity;

        public int Quantity
        {
            get =>_quantity;
            set 
            { 
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderItemNavArgs> OrderItems { get; set; } = new ObservableCollection<OrderItemNavArgs>();

        public void OnNavigatedTo(INavigation navigation, OrderItemNavArgs parameter)
        {
            var orderItem = parameter;
            Product = orderItem.Product;
            Quantity = orderItem.Quantity;
            OrderItems.Add(new OrderItemNavArgs { Product = Product, Quantity = Quantity });
        }
        //public override void OnParameterChanged(object parameter)
        //{
        //    if (parameter is OrderItemNavArgs)
        //    {
        //        var orderItem = (OrderItemNavArgs)parameter;
        //        Product = orderItem.Product;
        //        Quantity = orderItem.Quantity;
        //        OrderItems.Add(new OrderItemNavArgs {Product = Product, Quantity = Quantity });
        //    }

        //}
    }
}
