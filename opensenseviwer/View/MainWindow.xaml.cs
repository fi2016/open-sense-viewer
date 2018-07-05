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
using LiveCharts;
using LiveCharts.Wpf;

namespace View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] sensors;
        private ViewModel.ViewModel vm;
        private char splitter = ';';

        public MainWindow(ViewModel.ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

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

        private void einlesen(string tempSensor, string humidSensor)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "temperature",
                    Values = vm.GetData(tempSensor),
                    PointGeometry = null
                    
                },
                new LineSeries
                {
                    Title = "humidity",
                    Values = vm.GetData(humidSensor),
                    PointGeometry = null
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString(".00");

            DataContext = this;
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
           einlesen();
        }

        private void einlesen()
        {
            sensors = vm.getSensors().Split(splitter);
            einlesen((string)sensors.GetValue(0), (string)sensors.GetValue(1));
        }

        private void Button_Credits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sebastian Mikolai\nFrank Baumeister\nCarina Jörg");
        }
    }
}
