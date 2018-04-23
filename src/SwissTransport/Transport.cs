using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace SwissTransport
{
    public class Transport : ITransport
    {
        public Stations GetStations(string query)
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/locations?query=" + query + "&type=station");
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var message = new StreamReader(responseStream).ReadToEnd();
                var stations = JsonConvert.DeserializeObject<Stations>(message
                    , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return stations;
            }

            return null;
        }

        public StationBoardRoot GetStationBoard(string station)
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/stationboard?station=" + station + "&limit=10");
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var readToEnd = new StreamReader(responseStream).ReadToEnd();
                var stationboard = new StationBoardRoot();
                try
                {
                    stationboard = JsonConvert.DeserializeObject<StationBoardRoot>(readToEnd);
                }
                catch
                {
                    // do nothing
                }
                return stationboard;
            }

            return null;
        }

        public Connections GetConnections(string fromStation, string toStattion, string departDate, string departTime)
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/connections?from=" + fromStation + "&to=" + toStattion + "&date=" + departDate + "&time=" + departTime);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var readToEnd = new StreamReader(responseStream).ReadToEnd();
                var connections = new Connections();
                try
                {
                    connections = JsonConvert.DeserializeObject<Connections>(readToEnd);
                } catch
                {
                    // do nothing
                }
                return connections;
            }

            return null;
        }

        private static WebRequest CreateWebRequest(string url)
        {
            var request = WebRequest.Create(url);
            var webProxy = WebRequest.DefaultWebProxy;

            webProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = webProxy;
            
            return request;
        }
    }
}
