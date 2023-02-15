using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Window
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton== MouseButtonState.Pressed)
            { 
                DragMove(); 
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HowToButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
