using Mazada.Model;
using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mazada.ViewModel
{
    class ProductCollectionViewModel : ViewModelBase
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
        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set 
            { 
                _selectedProduct = value;
                OnPropertyChanged();
                Navigation.GetInstance().NavigateTo<ProductDetailView>(_selectedProduct);
            }
        }

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private MySQLRepository<Product> productRepo = new MySQLRepository<Product>();
        public ProductCollectionViewModel()
        {
            LoadProduct();
        }

        public RelayCommand SearchCommand => new RelayCommand(e => Search());
        private async void Search()
        {
            Products.Clear();
            var products = await productRepo.GetAllAsync();
            products = products.Where(p => p.Name.ToLower().Contains(SearchText));

            foreach (var prod in products)
            {
                Products.Add(prod);
            }
        }

        private async void LoadProduct()
        {
            var products = await productRepo.GetAllAsync();

            foreach (var prod in products)
            {
                Products.Add(prod);
            }
        }

        public override void OnParameterChanged(object parameter)
        {
            SearchText = (string)parameter;
            
        }
    }
}
