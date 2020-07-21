using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistanceMeasurerService.Models;
using DistanceMeasurerService.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DistanceMeasurerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private IAirportCoordinateProvider _airportCoordinateProvider = null;
        private IDistanceMeasurer _distanceMeasurer = null;

        public AirportsController(IAirportCoordinateProvider airportCoordinateProvider, IDistanceMeasurer distanceMeasurer)
        {
            this._airportCoordinateProvider = airportCoordinateProvider;
            this._distanceMeasurer = distanceMeasurer;
        }

        // GET api/Airports/MOW/PER
        [HttpGet("{airport1}/{airport2}")]
        public async Task<IActionResult> Get(string airport1, string airport2)
        {
            AirportCoordinate[] airports = new AirportCoordinate[2] { new AirportCoordinate() { AirportCode = airport1 }, new AirportCoordinate(){ AirportCode = airport2} };
            List<Task> tasks = new List<Task>();

            foreach (var airportCoordinate in airports)
            {
                try
                {
                    var coordinate = _airportCoordinateProvider.GetCoordinate(airportCoordinate.AirportCode).ContinueWith(x =>
                    {
                        if (x.IsCompletedSuccessfully && x.Result != null)
                        {
                            airportCoordinate.Coordinate = x.Result;
                        }
                    });
                    tasks.Add(coordinate);
                }
                catch
                {
                    return BadRequest();
                }
            }
            await Task.WhenAll(tasks);

            return Ok((int)Math.Round(_distanceMeasurer.CalculateDistanceMiles(airports.Select(x=>x.Coordinate).ToArray())));
        }
    }
}
