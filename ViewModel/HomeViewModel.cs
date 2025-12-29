using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Search()
        {
            var nav = Navigation.GetInstance();
            nav.NavigateTo<ProductCollectionViewModel>(SearchText);
        }

    }
}
