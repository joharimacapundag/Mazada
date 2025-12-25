using Mazada.ViewModel;
using System.Windows;

namespace Mazada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            UserViewModel userViewModel = new UserViewModel();
            DataContext = userViewModel;

        }
    }
}
