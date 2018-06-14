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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            Button_MenuOpen.Visibility = Visibility.Collapsed;
            Button_MenuClose.Visibility = Visibility.Visible;
        }

        private void Button_MenuClose_Click(object sender, RoutedEventArgs e)
        {
            Button_MenuOpen.Visibility = Visibility.Visible;
            Button_MenuClose.Visibility = Visibility.Collapsed;
        }
    }
}
