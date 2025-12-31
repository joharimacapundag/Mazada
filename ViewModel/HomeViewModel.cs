using Mazada.Services;

namespace Mazada.ViewModel
{
    class HomeViewModel : ViewModelBase
    {
        private string _searchText;
        private int _cartSize;
        public int CartSize
        {
            get => _cartSize;
            set
            {
                _cartSize = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _currentViewModel;

        public RelayCommand SearchCommand => new RelayCommand(e => _stackNavigation.NavigateTo<ProductCollectionViewModel, string>(SearchText));
        private INavigation _stackNavigation = new StackNavigation();
        public string SearchText
        {
            get => _searchText;
            set 
            { 
                _searchText = value;
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
        public HomeViewModel()
        {
            _stackNavigation.ViewModelChanged += OnViewModelChanged;
        }
        public void OnViewModelChanged(ViewModelBase viewModel) => CurrentViewModel = viewModel;


    }
}
