using DistanceMeasurerService.Models.CTeleport.Entities;
using DistanceMeasurerService.Models.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models.CTeleport
{
    public class CtAirportCoordinateProvider : IAirportCoordinateProvider
    {
        public const string CTELEPORT_ENDPOINT_URL = "https://places-dev.cteleport.com/airports";

        public async Task<Coordinate> GetCoordinate(string airportIataCode)
        {
            AirportInfo airportInfo = await this.GetAirportInfo(airportIataCode); ;
            if (airportInfo?.Location == null)
                throw new Exception("Airport Information does not contain Location info");

            return new Coordinate() { Latitude = airportInfo.Location.Lat, Longitude = airportInfo.Location.Lon };
        }

        public async Task<AirportInfo> GetAirportInfo(string airportIataCode)
        {
            SimpleHttpCommunicator httpCommunicator = new SimpleHttpCommunicator();
            string airportInfoString = await httpCommunicator.SendGetRequest(String.Format("{0}/{1}", CTELEPORT_ENDPOINT_URL, airportIataCode));
            AirportInfo airportInfo = null;
            try
            {
                airportInfo = JsonConvert.DeserializeObject<AirportInfo>(airportInfoString);
            }
            catch
            {
                throw new Exception("Unable to retreive Airport Information from CTeleport endpoint. Response is corrupted");
            }
            return airportInfo;
        }
    }
}
