using System;
using System.Collections.Generic;
using System.Device.Location;

namespace SwissTransportGUI
{
    class GeoLocationHelper
    {
        private GeoCoordinateWatcher _geoWatcher;
        private GeoCoordinate _coordinate = null;

        public GeoLocationHelper()
        {
            UpdateEventHandler = new List<GeoLocationUpdateEvent>();
            _geoWatcher = new GeoCoordinateWatcher();
            _geoWatcher.MovementThreshold = 20;
            _geoWatcher.StatusChanged += OnStatusChange;
            _geoWatcher.PositionChanged += OnPositionChange;

            _geoWatcher.Start(false);
        }

        public GeoCoordinate GeoCoordinate
        {
            get { return _coordinate; }
        }

        /// <summary>
        /// Liste, in welche alle EventHandler eingetragen werden können
        /// </summary>
        public List<GeoLocationUpdateEvent> UpdateEventHandler { get; set; }

        private void OnStatusChange(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Console.Write(e.Status);
            if (e.Status == GeoPositionStatus.Ready)
            {
                if (!(_geoWatcher.Position.Location.IsUnknown))
                {
                    _coordinate = _geoWatcher.Position.Location;
                    CallUpdateEvent();
                }
            }
        }

        private void OnPositionChange(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!(e.Position.Location.IsUnknown))
            {
                _coordinate = e.Position.Location;
                CallUpdateEvent();
            }
        }

        private void CallUpdateEvent()
        {
            foreach (GeoLocationUpdateEvent updateEvent in UpdateEventHandler)
            {
                updateEvent.OnGeoLocationUpdate(GeoCoordinate.Latitude.ToString("###.##############"), GeoCoordinate.Longitude.ToString("###.##############"));
            }
        }
    }
}
