using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models
{
    public interface IAirportCoordinateProvider
    {
        Task<Coordinate> GetCoordinate(string airportIataCode);
    }
}
