using PropertyChanged;
using SwissTransport;
using System.Collections.ObjectModel;

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

        public void UpdateConnections(string location)
        {
            Connections.Clear();

            StationBoardRoot searchResult = _transport.GetStationBoard(location);
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
