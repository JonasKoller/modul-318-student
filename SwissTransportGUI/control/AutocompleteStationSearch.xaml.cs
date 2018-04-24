using SwissTransportGUI.viewmodel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SwissTransportGUI
{
    /// <summary>
    /// Interaktionslogik für AutocompleteStationSearch.xaml
    /// </summary>
    public partial class AutocompleteStationSearch : UserControl
    {
        private AutocompleteStationSearchViewModel _viewModel;
        public AutocompleteStationSearch()
        {
            InitializeComponent();

            _viewModel = new AutocompleteStationSearchViewModel();
            this.DataContext = _viewModel;
        }

        /// <summary>
        /// Methode, welche den Stationsnamen zurückgibt, welcher im AutocompleteStationSearch ausgesucht ist
        /// <returns>Stationsname</returns>  
        /// </summary>
        public string Location
        {
            get { return _viewModel.LocationSearchString; }
        }

        private void SearchComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).IsDropDownOpen = true;
        }

        private void SearchComboBox_TextChanged(object sender, RoutedEventArgs e)
        {
            string locationString = (sender as ComboBox).Text;
            try
            {
                _viewModel.UpdateLocationSearchPreviewItems(locationString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nFehlercode für Profis: " + ex.InnerException.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
