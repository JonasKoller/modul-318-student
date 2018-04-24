using PropertyChanged;
using SwissTransport;
using System;
using System.Collections.ObjectModel;

namespace SwissTransportGUI.viewmodel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class FahrplanViewModel
    {
        private Transport _transport;
        public FahrplanViewModel()
        {
            _transport = new Transport();
            Connections = new ObservableCollection<DisplayConnection>();
            DepartDate = DateTime.Now;
            DepartTime = DateTime.Now.ToString("HH:mm");
        }

        public DateTime DepartDate { set; get; }

        public string DepartTime { set; get; }

        public ObservableCollection<DisplayConnection> Connections { get; set; }

        public void UpdateConnections(string fromLocation, string toLocation)
        {
            Connections.Clear();

            string departDate = DepartDate.ToString("yyyy-MM-dd");

            Connections searchResult = _transport.GetConnections(fromLocation, toLocation, departDate, DepartTime);
            if (searchResult == null || searchResult.ConnectionList == null)
                return;

            foreach (Connection c in searchResult.ConnectionList)
            {
                DisplayConnection dpCon = new DisplayConnection(c.From.Departure, c.To.Arrival, c.Duration, c.From.Platform);
                Connections.Add(dpCon);
            }
        }

        public string GetMailToString(string fromLocation, string toLocation)
        {
            string subject = "ÖV-Verbindungen";
            string body = "ÖV-Verbindungen%20von%20" + fromLocation + "%20nach%20" + toLocation + "%20am%20" + DepartDate.ToString("dd.MM.yyyy") + "%20um%20" + DepartTime + "%0D%0A%0D%0A";

            UpdateConnections(fromLocation, toLocation);

            foreach (DisplayConnection dc in Connections)
            {
                body += "Abfahrt:%20" + dc.Departure + ",%20Ankunft:%20" + dc.Arrival + ",%20Dauer:%20" + dc.Duration + "%0D%0A";
            }

            return "mailto:?subject=" + subject + "&body=" + body;
        }
    }
}
