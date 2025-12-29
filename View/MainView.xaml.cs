using Mazada.ViewModel;
using System.Windows;

namespace Mazada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
       
        public MainView()
        {
            InitializeComponent();
            var currentViewModel = new MainViewModel();
            DataContext = currentViewModel;
        }
    }
}
