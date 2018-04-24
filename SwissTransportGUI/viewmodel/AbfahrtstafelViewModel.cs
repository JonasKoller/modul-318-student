using PropertyChanged;
using SwissTransport;
using System;
using System.Collections.ObjectModel;
using System.Net;

namespace SwissTransportGUI.viewmodel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class AbfahrtstafelViewModel
    {
        private Transport _transport;
        public AbfahrtstafelViewModel()
        {
            _transport = new Transport();
            Connections = new ObservableCollection<StationBoardConnection>();
        }

        public ObservableCollection<StationBoardConnection> Connections { get; set; }

        /// <summary>
        /// Lädt die Abfahrten für eine bestimmte Station, welche als Parameter übergeben wird
        /// Durch das Binding werden auf dem GUI alle neu geladenen Verbindungen direkt angezeigt
        /// </summary>
        /// <param name="location">Stationsname</param>
        public void UpdateConnections(string location)
        {
            Connections.Clear();

            StationBoardRoot searchResult = null;
            try
            {
                searchResult = _transport.GetStationBoard(location);
            }
            catch (WebException ex)
            {
                throw new Exception("Fehler bei der Verbindung. Bitte prüfe deine Internetverbindung.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ein unbekannter Fehler ist aufgetreten. Bitte überprüfen Sie Ihre Angaben und probieren Sie es erneut. Sollte der Fehler erneut auftreten, wenden Sie sich an Ihren Systemadministrator.", ex);
            }

            if (searchResult == null || searchResult.Entries == null)
                return;

            foreach (StationBoard s in searchResult.Entries)
            {
                string formattedDate = "";
                if (s.Stop.Departure != null)
                    formattedDate = s.Stop.Departure.ToString("dd.MM.yyyy HH:mm");
                StationBoardConnection sbCon = new StationBoardConnection(s.Name, s.Number, s.To, formattedDate);
                Connections.Add(sbCon);
            }
        }
    }
}
