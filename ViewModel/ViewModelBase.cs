using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mazada.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        //CallerMemberName automatically assign a value from class property in compile time
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
