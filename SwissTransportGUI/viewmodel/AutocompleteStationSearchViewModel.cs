using PropertyChanged;
using SwissTransport;
using System.Collections.ObjectModel;

namespace SwissTransportGUI.viewmodel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class AutocompleteStationSearchViewModel
    {
        private Transport _transport;
        public AutocompleteStationSearchViewModel()
        {
            _transport = new Transport();
            LocationSearchPreviewItems = new ObservableCollection<string>();
        }

        public string LocationSearchString { get; set; }
        public ObservableCollection<string> LocationSearchPreviewItems { get; set; }

        public void UpdateLocationSearchPreviewItems(string location)
        {
            LocationSearchPreviewItems.Clear();
            LocationSearchString = location;

            Stations searchResult = _transport.GetStations(LocationSearchString);
            if (searchResult == null || searchResult.StationList == null)
                return;

            foreach (Station s in searchResult.StationList)
            {
                LocationSearchPreviewItems.Add(s.Name);
            }
        }
    }
}
