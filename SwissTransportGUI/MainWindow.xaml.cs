using SwissTransport;
using SwissTransportGUI.viewmodel;
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

namespace SwissTransportGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FahrplanViewModel _viewModelFahrplan;
        private AbfahrtstafelViewModel _viewModelAbfahrtstafel;
        private Transport _transport;

        public MainWindow()
        {
            InitializeComponent();

            _viewModelFahrplan = new FahrplanViewModel();
            _viewModelAbfahrtstafel = new AbfahrtstafelViewModel();
            _transport = new Transport();

            this.DataContext = _viewModelFahrplan;
        }

        private void FromSearchbox_TextChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (sender as ComboBox);
            _viewModelFahrplan.FromSearchString = cBox.Text;
            
            _viewModelFahrplan.FromSearchPreviewItems.Clear();
            Stations searchResult = _transport.GetStations(_viewModelFahrplan.FromSearchString);
            if (searchResult == null)
                return;

            foreach(Station s in searchResult.StationList)
            {
               _viewModelFahrplan.FromSearchPreviewItems.Add(s.Name);
            }
        }

        private void ToSearchbox_TextChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (sender as ComboBox);
            _viewModelFahrplan.ToSearchString = cBox.Text;

            _viewModelFahrplan.ToSearchPreviewItems.Clear();
            Stations searchResult = _transport.GetStations(_viewModelFahrplan.ToSearchString);
            if (searchResult == null)
                return;

            foreach (Station s in searchResult.StationList)
            {
                _viewModelFahrplan.ToSearchPreviewItems.Add(s.Name);
            }
        }

        private void LocationSearchCombobox_TextChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (sender as ComboBox);
            _viewModelAbfahrtstafel.LocationSearchString = cBox.Text;

            _viewModelAbfahrtstafel.LocationSearchPreviewItems.Clear();
            Stations searchResult = _transport.GetStations(_viewModelAbfahrtstafel.LocationSearchString);
            if (searchResult == null)
                return;

            foreach (Station s in searchResult.StationList)
            {
                _viewModelAbfahrtstafel.LocationSearchPreviewItems.Add(s.Name);
            }
        }

        private void Searchbox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).IsDropDownOpen = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModelFahrplan.Connections.Clear();
            Connections searchResult = _transport.GetConnections(_viewModelFahrplan.FromSearchString, _viewModelFahrplan.ToSearchString, _viewModelFahrplan.DepartDate.ToString("yyyy-MM-dd"), _viewModelFahrplan.DepartTime);
            if (searchResult == null || searchResult.ConnectionList == null)
                return;

            foreach (Connection c in searchResult.ConnectionList)
            {
                DisplayConnection dpCon = new DisplayConnection(c.From.Departure, c.To.Arrival, c.Duration, c.From.Platform);
                _viewModelFahrplan.Connections.Add(dpCon);
            }
        }

        private void TabChangeButtonFahrplan_Click(object sender, RoutedEventArgs e)
        {
            (this.FindName("TabController") as TabControl).SelectedIndex = 0;
            this.DataContext = _viewModelFahrplan;
        }

        private void TabChangeButtonFahrplan1_Click(object sender, RoutedEventArgs e)
        {
            (this.FindName("TabController") as TabControl).SelectedIndex = 1;
            this.DataContext = _viewModelAbfahrtstafel;
        }

        private void AbfahrtstafelLoadButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModelAbfahrtstafel.Connections.Clear();
            StationBoardRoot searchResult = _transport.GetStationBoard(_viewModelAbfahrtstafel.LocationSearchString);

            if (searchResult == null || searchResult.Entries == null)
                return;

            foreach (StationBoard s in searchResult.Entries)
            {
                string formattedDate;
                if(s.Stop.Departure == null)
                {
                    formattedDate = "";
                }
                else
                {
                    formattedDate = String.Format("{0:dd/MM/yyyy HH:mm:ss}", s.Stop.Departure);
                }
                StationBoardConnection sbCon = new StationBoardConnection(s.Name, s.Number, s.To, formattedDate);
                _viewModelAbfahrtstafel.Connections.Add(sbCon);
            }
        }

        private void ShowLocationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
