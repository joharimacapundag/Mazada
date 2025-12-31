using Mazada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazada.Services
{
    interface INavigation
    {
        event Action<ViewModelBase> ViewModelChanged;
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase, new();
        void NavigateTo<TViewModel, TArgs>(TArgs parameter) where TViewModel : ViewModelBase, new();
        void GoBack();
    }
}
