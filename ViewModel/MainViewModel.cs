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

        public RelayCommand HomeCommand => new RelayCommand(e=> { Home(); });
        public RelayCommand AccountCommand => new RelayCommand(e => { Account();  });

        public MainViewModel()
        {
            CurrentViewModel = new HomeViewModel();
        }

        public void Home()
        {
            CurrentViewModel = new HomeViewModel();
            Console.WriteLine("Home");
        }

        public void Account()
        {
            CurrentViewModel = new AccountViewModel();
            Console.WriteLine("Account");
        }
    }
}
