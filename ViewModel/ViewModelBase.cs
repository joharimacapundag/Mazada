using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mazada.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private object _parameter;
        public object Parameter 
        {
            get => _parameter;
            set
            {
                _parameter = value;
                ParameterChanged?.Invoke(_parameter);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<object> ParameterChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
