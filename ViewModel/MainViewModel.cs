using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mazada.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private int _progressBarTask;
        private ViewModelBase _currentViewModel;
        public int ProgressBarTask
        {
            get => _progressBarTask;
            set
            {
                _progressBarTask = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private INavigation _stackNavigation = new StackNavigation();

        public MainViewModel()
        {
            _stackNavigation.ViewModelChanged += OnViewModelChanged;
            //Run();
        }
        private void OnViewModelChanged(ViewModelBase viewModel) => CurrentViewModel = viewModel;

        public RelayCommand LoginCommand => new RelayCommand(e => _stackNavigation.NavigateTo<LoginViewModel>());
        public RelayCommand HomeCommand => new RelayCommand(e => _stackNavigation.NavigateTo<HomeViewModel>());
        public RelayCommand ProductsCommand => new RelayCommand(e => _stackNavigation.NavigateTo<ProductCollectionViewModel>());
        public RelayCommand ProductCommand => new RelayCommand(e => _stackNavigation.NavigateTo<ProductDetailView>());
        public RelayCommand BackCommand => new RelayCommand(e => _stackNavigation.GoBack());

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
                _stackNavigation.NavigateTo<LoginViewModel>();
            }
        }
    }
}
