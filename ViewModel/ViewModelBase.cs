using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mazada.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
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
        public event ParameterEventHandler ParameterChanged;
      
        public delegate void ParameterEventHandler(object parameter);
        public abstract void OnParameterChanged(object parameter);
        //CallerMemberName automatically assign a value from class property in compile time
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {
            ParameterChanged += OnParameterChanged;
        }

      

       
    }
}
