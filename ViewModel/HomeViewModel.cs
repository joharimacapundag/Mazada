using Mazada.Services;

namespace Mazada.ViewModel
{
    class HomeViewModel : ViewModelBase
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

        public RelayCommand SearchCommand => new RelayCommand(e => Search());

        public override void OnParameterChanged(object parameter)
        {
        }

        private void Search()
        {
            Navigation.GetInstance().NavigateTo<ProductCollectionViewModel>(SearchText);
        }

    }
}
