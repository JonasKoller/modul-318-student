using SwissTransportGUI.viewmodel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SwissTransportGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FahrplanViewModel _viewModelFahrplan;
        private AbfahrtstafelViewModel _viewModelAbfahrtstafel;
        private StationViewModel _viewModelStation;

        public MainWindow()
        {
            InitializeComponent();

            _viewModelFahrplan = new FahrplanViewModel();
            _viewModelAbfahrtstafel = new AbfahrtstafelViewModel();
            _viewModelStation = new StationViewModel();

            this.DataContext = _viewModelFahrplan;
        }

        private void TimetableSearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModelFahrplan.UpdateConnections(FromSearchBox.Location, ToSearchBox.Location);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nFehlercode für Profis: " + ex.InnerException.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void TabChangeButtonStation_Click(object sender, RoutedEventArgs e)
        {
            (this.FindName("TabController") as TabControl).SelectedIndex = 2;
            this.DataContext = _viewModelStation;
        }

        private void AbfahrtstafelLoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModelAbfahrtstafel.UpdateConnections(DepartboardSearchBox.Location);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nFehlercode für Profis: " + ex.InnerException.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StationOpenBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(StationSearchBox.Location))
                return;
            string urlLocation = Uri.EscapeDataString(StationSearchBox.Location);
            _viewModelStation.StationUrl = "https://www.google.com/maps/search/?api=1&query=" + urlLocation;

            if (String.IsNullOrEmpty(_viewModelStation.StationUrl))
                return;

            System.Diagnostics.Process.Start(_viewModelStation.StationUrl);
        }

        private void TimetableEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_viewModelFahrplan.GetMailToString(FromSearchBox.Location, ToSearchBox.Location));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nFehlercode für Profis: " + ex.InnerException.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
