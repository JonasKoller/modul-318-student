using PropertyChanged;
using SwissTransport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            _geoHelper.UpdateEvent.Add(this);
            NearStations = new ObservableCollection<Station>();
        }
        public string StationUrl { set; get; }

        public ObservableCollection<Station> NearStations { get; set; }

        public void OnGeoLocationUpdate(string latitude, string longitude)
        {
            NearStations.Clear();

            Stations searchResult = _transport.GetStations(latitude, longitude);
            if (searchResult == null || searchResult.StationList == null)
                return;

            foreach (Station s in searchResult.StationList)
            {
                NearStations.Add(s);
            }
        }
    }
}
