using PropertyChanged;
using SwissTransport;
using System;
using System.Collections.ObjectModel;
using System.Net;

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
            IsDepartTime = true;
        }

        public DateTime DepartDate { set; get; }

        public string DepartTime { set; get; }

        public ObservableCollection<DisplayConnection> Connections { get; set; }

        public bool IsDepartTime { get; set; }

        /// <summary>
        /// Lädt die ÖV-Verbingungen neu, welche nachher dargestellt. 
        /// Es werden dabei die Abfahrts-, und Ankunftsstation benötigt, zudem wird die Abfahrtszeit und das Datum beachtet.
        /// Es kann auch angegeben werden, ob es sich um die Abfahrts- oder Ankunftszeit handelt
        /// </summary>
        /// <param name="fromLocation">Stationsname des Abfahrtsorts</param>
        /// <param name="toLocation">Stationsname des Zielorts</param>
        public void UpdateConnections(string fromLocation, string toLocation)
        {
            Connections.Clear();

            string departDate = DepartDate.ToString("yyyy-MM-dd");

            Connections searchResult = null;

            try
            {
                searchResult = _transport.GetConnections(fromLocation, toLocation, departDate, DepartTime, IsDepartTime);
            }
            catch (WebException ex)
            {
                throw new Exception("Fehler bei der Verbindung. Bitte prüfe deine Internetverbindung.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ein unbekannter Fehler ist aufgetreten. Bitte überprüfen Sie Ihre Angaben und probieren Sie es erneut. Sollte der Fehler erneut auftreten, wenden Sie sich an Ihren Systemadministrator.", ex);
            }


            if (searchResult == null || searchResult.ConnectionList == null)
                return;

            foreach (Connection c in searchResult.ConnectionList)
            {
                DisplayConnection dpCon = new DisplayConnection(c.From.Departure, c.To.Arrival, c.Duration, c.From.Platform);
                Connections.Add(dpCon);
            }
        }

        /// <summary>
        /// Generiert einen MailTo-Link. Dieser kann mit einem Browser geöffnet werden und öffnet danach direkt das Standart-Mailprogramm.
        /// Im Mail wird der Betreff, sowie der Inhalt bereits abgefüllt mit den entsprechenden Informationen (Verbindungen wann wohin)
        /// </summary>
        /// <param name="fromLocation">Stationsname des Abfahrtsorts</param>
        /// <param name="toLocation">Stationsname des Zielorts</param>
        /// <returns>MailTo-Link mit aktuellen Verbindungen</returns>
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
