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
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private MySQLRepository<Product> productRepo = new MySQLRepository<Product>();
        public ProductCollectionViewModel()
        {
            //On parameter changed
            ParameterChanged += param => SearchText = (string)param;
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
    }
}
