using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using Model;

namespace View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel.ViewModel vm;
        private List<Sensor> listOfSensors;

        public MainWindow(ViewModel.ViewModel vm)
        {
            InitializeComponent();
            Vm = vm;
            GridMenu.Width = 50;
            Button_MenuOpen.Visibility = Visibility.Visible;
            Button_MenuClose.Visibility = Visibility.Collapsed;
            listViewSensors.Visibility = Visibility.Collapsed;
            label_Sensor.Visibility = Visibility.Collapsed;
            ListOfSensors = new List<Sensor>();
            List<Sensor> sensors = Vm.GetAllSensors();
            if (sensors != null)
            {
                foreach (Sensor sensor in sensors)
                {
                    listViewSensors.Items.Add(sensor);
                }
            }
            SeriesCollection = new SeriesCollection();
            comboBoxGroup.SelectedIndex = 0;
        }

        private ViewModel.ViewModel Vm
        {
            get
            {
                return vm;
            }

            set
            {
                vm = value;
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> YFormatter { get; set; }

        private List<Sensor> ListOfSensors { get => listOfSensors; set => listOfSensors = value; }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            DragMove();
        }

        private void button_minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            Button_MenuOpen.Visibility = Visibility.Collapsed;
            Button_MenuClose.Visibility = Visibility.Visible;
            listViewSensors.Visibility = Visibility.Visible;
            label_Sensor.Visibility = Visibility.Visible;
        }

        private void Button_MenuClose_Click(object sender, RoutedEventArgs e)
        {
            Button_MenuOpen.Visibility = Visibility.Visible;
            Button_MenuClose.Visibility = Visibility.Collapsed;
            listViewSensors.Visibility = Visibility.Collapsed;
            label_Sensor.Visibility = Visibility.Collapsed;
        }

        private void Button_Credits_Click(object sender, RoutedEventArgs e)
        {
            ProjectInfoApiResponse projectInfo = Vm.GetProjectInfo();
            if (projectInfo != null)
            {
                string authors = "";
                foreach (string author in projectInfo.Authors)
                {
                    authors += author + "\n";
                }
                MessageBox.Show("Projekt:\n" + projectInfo.Title + "\n\nBeschreibung:\n" + projectInfo.Description + "\n\nAutoren:\n" + authors + "\nWebsite:\n" + 
                    projectInfo.Website + "\n\nVersion:\n" + projectInfo.Version, "Open Sense Viewer", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Fehler beim Auslesen der Information!", "Open Sense Viewer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_lesen_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();
            foreach (Sensor sensor in ListOfSensors)
            {
                AddGraphLine(sensor);
            }
        }

        private void listViewSensors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Sensor item in e.RemovedItems)
            {
                ListOfSensors.Remove(item);
            }
            foreach (Sensor item in e.AddedItems)
            {
                ListOfSensors.Add(item);
            }
            SeriesCollection.Clear();
        }

        private void AddGraphLine(Sensor sensor)
        {
            SeriesCollection.Add(new LineSeries
            {
                Title = sensor.Name,
                Values = Vm.GetData(sensor.Uuid, (string)comboBoxGroup.SelectedItem),
                PointGeometry = null
            });

            //Labels = new[] {
            //    "",
            //    "Feb",
            //    "Mar",
             //   "Apr",
              //  "May"
            //};

            YFormatter = value => value.ToString(".00");
            DataContext = this;
        }
    }
}
