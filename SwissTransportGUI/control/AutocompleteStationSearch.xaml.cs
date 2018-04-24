using SwissTransportGUI.viewmodel;
using System.Windows;
using System.Windows.Controls;


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
            _viewModel.UpdateLocationSearchPreviewItems(locationString);
        }
    }
}
