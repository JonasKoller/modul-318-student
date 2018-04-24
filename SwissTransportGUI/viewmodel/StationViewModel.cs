using PropertyChanged;
using SwissTransport;
using System.Collections.ObjectModel;

namespace SwissTransportGUI.viewmodel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class StationViewModel : GeoLocationUpdateEvent
    {
        private Transport _transport;
        private GeoLocationHelper _geoHelper;
        public StationViewModel()
        {
            _transport = new Transport();
            _geoHelper = new GeoLocationHelper();
            _geoHelper.UpdateEventHandler.Add(this);
            NearStations = new ObservableCollection<Station>();
        }
        public string StationUrl { set; get; }

        public ObservableCollection<Station> NearStations { get; set; }

        /// <summary>
        /// Schnittstelle zum GeoLocationHelper.
        /// Diese Methode wird ausgeführt, wenn neue Koordinaten des aktuellen Orts verfügbar sind.
        /// Diese Methode sollte nur durch den GeoLocationHelper ausgelöst werden.
        /// </summary>
        /// <param name="latitude">Breitengrade der aktuellen Position</param>
        /// <param name="longitude">Längengrade der aktuellen Position</param>
        public void OnGeoLocationUpdate(string latitude, string longitude)
        {
            NearStations.Clear();

            Stations searchResult = null;

            try
            {
                searchResult = _transport.GetStations(latitude, longitude);
            }
            catch
            {
                return;
            }

            if (searchResult == null || searchResult.StationList == null)
                return;

            foreach (Station s in searchResult.StationList)
            {
                NearStations.Add(s);
            }
        }
    }
}
