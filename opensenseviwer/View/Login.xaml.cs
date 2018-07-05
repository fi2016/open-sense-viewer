using System.Windows;
using System.Windows.Input;
using Model;

namespace View
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private ViewModel.ViewModel vm = new ViewModel.ViewModel();

        public Login()
        {
            InitializeComponent();
            Config config = vm.LoadConfig();
            if (config != null)
            {
                textbox_platform.Text = config.Platform;
                textbox_nickname.Text = config.Username;
            }
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Login(textbox_nickname.Text, textbox_password.Password, textbox_platform.Text))
            {
                this.Hide();
                MainWindow mw = new MainWindow(vm);
                mw.Show();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
