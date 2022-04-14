using PICalculator.App.ViewModels;
using System.Windows;

namespace PICalculator.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
