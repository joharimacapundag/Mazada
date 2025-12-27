using Mazada.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Mazada.Services
{
    class Navigation
    {
        private static Navigation _instance;
        private static Stack<ViewModelBase> _stackViews = new Stack<ViewModelBase>();
        private Navigation() { }
        public void NavigateTo<TViewModel>(MainViewModel mainViewModel) where TViewModel : ViewModelBase, new()
        {

            // Check if a ViewModel of this type already exists in the stack
            var existing = _stackViews.OfType<TViewModel>().FirstOrDefault();

            if (existing != null)
            {
                // Remove all above it
                while (_stackViews.Peek() != existing)
                    _stackViews.Pop();

                mainViewModel.CurrentViewModel = existing;
            }
            else
            {
                // Create new view model
                var viewModel = new TViewModel();
                _stackViews.Push(viewModel);
                mainViewModel.CurrentViewModel = viewModel;
            }
        }

        public void GoBack(MainViewModel mainViewModel)
        {
            if (_stackViews.Count > 1)
            {
                _stackViews.Pop();
                mainViewModel.CurrentViewModel = _stackViews.Peek();
            }
        }

        public static Navigation GetInstance()
        {
            return _instance != null ? _instance : _instance = new Navigation();
        }

    }
}
