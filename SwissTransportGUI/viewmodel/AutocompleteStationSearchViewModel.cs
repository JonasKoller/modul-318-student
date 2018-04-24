using PropertyChanged;
using SwissTransport;
using System;
using System.Collections.ObjectModel;
using System.Net;

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

        /// <summary>
        /// Lädt alle Stationsnamen neu, welche nachher als Vorschläge dargestellt werden
        /// </summary>
        /// <param name="location">Stationsname</param>
        public void UpdateLocationSearchPreviewItems(string location)
        {
            LocationSearchPreviewItems.Clear();
            LocationSearchString = location;

            Stations searchResult = null;
            try
            {
                searchResult = _transport.GetStations(LocationSearchString);
            }
            catch (WebException ex)
            {
                throw new Exception("Fehler bei der Verbindung. Bitte prüfe deine Internetverbindung.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ein unbekannter Fehler ist aufgetreten. Bitte überprüfen Sie Ihre Angaben und probieren Sie es erneut. Sollte der Fehler erneut auftreten, wenden Sie sich an Ihren Systemadministrator.", ex);
            }

            if (searchResult == null || searchResult.StationList == null)
                return;

            foreach (Station s in searchResult.StationList)
            {
                LocationSearchPreviewItems.Add(s.Name);
            }
        }
    }
}
