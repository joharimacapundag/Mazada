using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        private int _progressBarTask;

        public int ProgressBarTask
        {
            get => _progressBarTask; 
            set 
            { 
                _progressBarTask = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            Navigation.GetInstance().ViewModelChanged += vm => CurrentViewModel = vm;
        }

        public async Task Task1()
        {
            await Task.Delay(1000);
            ProgressBarTask += 1;
        }
        public async Task Task2()
        {
            await Task.Delay(2000);
            ProgressBarTask += 1;
        }
        public async Task Task3()
        {
            await Task.Delay(3000);
            ProgressBarTask += 1;
        }
        public async Task Task4()
        {
            await Task.Delay(4000);
            ProgressBarTask += 1;
        }

        public async void Run()
        {
            await Task1();
            await Task2();
            await Task3();
            await Task4();
            if (_progressBarTask == 4)
            {
                Navigation.GetInstance().NavigateTo<LoginViewModel>();
            }
        }
        public RelayCommand LoginCommand => new RelayCommand(e => Navigation.GetInstance().NavigateTo<LoginViewModel>());
        public RelayCommand HomeCommand => new RelayCommand(e => Navigation.GetInstance().NavigateTo<HomeViewModel>());
        public RelayCommand ProductsCommand => new RelayCommand(e => Navigation.GetInstance().NavigateTo<ProductCollectionViewModel>());
        public RelayCommand ProductCommand => new RelayCommand(e => Navigation.GetInstance().NavigateTo<ProductDetailView>());
        public RelayCommand BackCommand => new RelayCommand(e => Navigation.GetInstance().GoBack());

    }
}
