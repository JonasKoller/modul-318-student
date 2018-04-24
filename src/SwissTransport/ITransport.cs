namespace SwissTransport
{
    public interface ITransport
    {
        Stations GetStations(string query);
        Stations GetStations(string latitude, string longitude);
        StationBoardRoot GetStationBoard(string station);
        Connections GetConnections(string fromStation, string toStattion, string departDate, string departTime, bool isDepartTime);
    }
}