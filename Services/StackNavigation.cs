using Mazada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazada.Services
{
    class StackNavigation : INavigation
    {
        private readonly Stack<ViewModelBase> _stackViews = new Stack<ViewModelBase>();
        public event Action<ViewModelBase> ViewModelChanged;

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase, new()
        {
            var existing = _stackViews.OfType<TViewModel>().FirstOrDefault();

            ViewModelBase viewModel;

            if (existing != null)
            {
                // Remove all above it
                while (_stackViews.Peek() != existing)
                    _stackViews.Pop();
                viewModel = existing;
            }
            else
            {
                // Create new view model
                viewModel = new TViewModel();
                _stackViews.Push(viewModel);
            }

            ViewModelChanged?.Invoke(viewModel);

        }

        public void NavigateTo<TViewModel, TArgs>(TArgs parameter) where TViewModel : ViewModelBase, new()
        {
            var existing = _stackViews.OfType<TViewModel>().FirstOrDefault();

            ViewModelBase viewModel;

            if (existing != null)
            {
                // Remove all above it
                while (_stackViews.Peek() != existing)
                    _stackViews.Pop();
                viewModel = existing;
            }
            else
            {
                // Create new view model
                viewModel = new TViewModel();
                _stackViews.Push(viewModel);
            }

            ViewModelChanged?.Invoke(viewModel);

            if (viewModel is INavigationAware<TArgs> viewModelAware)
                viewModelAware.OnNavigatedTo(this, parameter);

        }
        public void GoBack()
        {
            if (_stackViews.Count > 1)
            {
                _stackViews.Pop();
                var viewModel = _stackViews.Peek();
                ViewModelChanged?.Invoke(viewModel);
            }
        }


    }
}
