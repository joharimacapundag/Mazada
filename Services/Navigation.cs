using Mazada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazada.Services
{
    class Navigation
    {
        private static Navigation _instance;
        private static readonly Stack<ViewModelBase> _stackViews = new Stack<ViewModelBase>();
        private Navigation(){}

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                ViewModelChanged?.Invoke(_currentViewModel);
            }
        }

        public event Action<ViewModelBase> ViewModelChanged;

        public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase, new()
        {
            // Check if a ViewModel of this type already exists in the stack
            var existing = _stackViews.OfType<TViewModel>().FirstOrDefault();

            if (existing != null)
            {
                // Remove all above it
                while (_stackViews.Peek() != existing)
                    _stackViews.Pop();

                CurrentViewModel = existing;
                CurrentViewModel.Parameter = parameter;
            }
            else
            {
                // Create new view model
                var viewModel = new TViewModel();
                _stackViews.Push(viewModel);

                CurrentViewModel = viewModel;
                CurrentViewModel.Parameter = parameter;
            }
        }
        public void GoBack()
        {
            if (_stackViews.Count > 1)
            {
                _stackViews.Pop();
                CurrentViewModel = _stackViews.Peek();
            }
        }

        public static Navigation GetInstance()
        {
            return _instance ?? (_instance = new Navigation());
        }

    }
}
