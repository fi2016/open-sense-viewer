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
        ViewModel.ViewModel vm;
        public MainWindow(ViewModel.ViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            einlesen("a43ae11b-6c16-11e8-b35f-b0e87cb20b1d", "482a98b4-6cc2-11e8-b35f-b0e87cb20b1d");
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

        private void einlesen(string humidSensor, string tempSensor)
        {
            ChartValues<float> temp = vm.GetData("a43ae11b-6c16-11e8-b35f-b0e87cb20b1d");
            ChartValues<float> hum = vm.GetData("482a98b4-6cc2-11e8-b35f-b0e87cb20b1d");
            //ChartValues<float> temp = vm.GetData("tempSensor");
            //ChartValues<float> hum = vm.GetData("humidSensor");
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "temperature",
                    Values = temp,
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "himidity",
                    Values = hum,
                    PointGeometry = null
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString(".00");

            DataContext = this;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Credits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sebastian Mikolai\nFrank Baumeister\nCarina Jörg");
        }
    }
}
