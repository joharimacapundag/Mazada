using Mazada.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazada.ViewModel
{
    class CartViewModel : ViewModelBase
    {
        public ObservableCollection<CartNavArgs> Cart = new ObservableCollection<CartNavArgs>();
    }
}
