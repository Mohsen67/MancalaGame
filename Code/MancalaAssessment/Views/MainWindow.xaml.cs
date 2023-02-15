using MancalaAssessment.ViewModels;
using System.Windows;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mw = new MainWindowViewModel(new Models.GameEngine());
            this.DataContext = mw;
        }

    }
}
