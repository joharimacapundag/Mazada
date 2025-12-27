using Mazada.Services;
using System;

namespace Mazada.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set 
            { 
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand => new RelayCommand(e => ShowLogin());
        public RelayCommand HomeCommand => new RelayCommand(e => ShowHome());
        public RelayCommand ProductsCommand => new RelayCommand(e => ShowProducts());
        public RelayCommand ProductCommand => new RelayCommand(e => ShowProduct());
        public RelayCommand BackCommand => new RelayCommand(e => GoBack());

        public void ShowLogin()
        {
            var nav = Navigation.GetInstance();
            nav.NavigateTo<LoginViewModel>(this);
        }
        public void ShowHome()
        {
            var nav = Navigation.GetInstance();
            nav.NavigateTo<HomeViewModel>(this);
        }

        public void ShowProducts()
        {
            var nav = Navigation.GetInstance();
            nav.NavigateTo<ProductCollectionViewModel>(this);
        }

        public void ShowProduct()
        {
            var nav = Navigation.GetInstance();
            nav.NavigateTo<ProductDetailView>(this);
        }

        public void GoBack()
        {
            var nav = Navigation.GetInstance();
            nav.GoBack(this);
        }
    }
}
