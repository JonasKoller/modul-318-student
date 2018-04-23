using System;

namespace SwissTransportGUI
{
    class StationBoardConnection
    {
        private string _name;
        private string _number;
        private string _to;
        private string _departure;

        public StationBoardConnection() { }

        public StationBoardConnection(string name, string number, string to, string departure)
        {
            Name = name;
            Number = number;
            To = to;
            Departure = departure;
        }

        public string Name
        {
            get { return _name; }
            set { _name = String.IsNullOrEmpty(value) ? "" : value; }
        }

        public string Number
        {
            get { return _number; }
            set { _number = String.IsNullOrEmpty(value) ? "" : value; }
        }

        public string To
        {
            get { return _to; }
            set { _to = String.IsNullOrEmpty(value) ? "" : value; }
        }

        public string Departure
        {
            get { return _departure; }
            set { _departure = String.IsNullOrEmpty(value) ? "" : value; }
        }
    }
}
