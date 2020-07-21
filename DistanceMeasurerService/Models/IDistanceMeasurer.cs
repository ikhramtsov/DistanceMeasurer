using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models
{
    /// <summary>
    /// Applicable for connection flights
    /// </summary>
    public interface IDistanceMeasurer
    {
        /// <summary>
        /// Calculate distance between points in miles
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        double CalculateDistanceMiles(params Coordinate[] points);
    }
}
