using System;

namespace SwissTransportGUI
{
    class DisplayConnection
    {
        private string _departure;
        private string _arrival;
        private string _duration;
        private string _platform;

        public DisplayConnection() { }

        public DisplayConnection(string departure, string arrival, string duration, string platform)
        {
            Departure = departure;
            Arrival = arrival;
            Duration = duration;
            Platform = platform;
        }

        public string Departure
        {
            get { return _departure; }
            set { _departure = getFormattedDate(value); }
        }
        public string Arrival
        {
            get { return _arrival; }
            set { _arrival = getFormattedDate(value); }
        }
        public string Duration
        {
            get { return _duration; }
            set { _duration = String.IsNullOrEmpty(value) ? "" : value.Split('d')[1]; }
        }
        public string Platform
        {
            get { return _platform; }
            set { _platform = String.IsNullOrEmpty(value) ? "" : value; }
        }

        private string getFormattedDate(string value)
        {
            if (String.IsNullOrEmpty(value))
                return "";
            DateTime date;
            if (!DateTime.TryParse(value, out date))
                return "";
            return String.Format("{0:dd/MM/yyyy HH:mm:ss}", date);
        }
    }
}
