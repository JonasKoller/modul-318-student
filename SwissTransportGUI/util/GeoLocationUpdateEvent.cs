using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGUI
{
    interface GeoLocationUpdateEvent
    {
        void OnGeoLocationUpdate(string latitude, string longitude);
    }
}
