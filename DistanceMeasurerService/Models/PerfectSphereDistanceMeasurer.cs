using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models
{
    /// <summary>
    /// Class measures distance between points using algorythm which assumes that Earth is a perfect sphere (which is not true, so result won't be 100% accurate). Calulcation made in double variable, not decimal
    /// </summary>
    public class PerfectSphereDistanceMeasurer : IDistanceMeasurer
    {
        public const double EARTH_RADIUS_KM = 6371.21;
        public const double MILES_PER_KILOMETER = 1.60934;

        public double CalculateDistanceMiles(params Coordinate[] points)
        {
            if (points?.Length < 2)
                throw new ArgumentException();

            double distance = 0;
            for (int i = 0; i < points.Length - 1; i++)
                distance += this.CalcDistanceMiles(points[i], points[i+1]);

            return distance;
        }

        private double CalcDistanceMiles(Coordinate pointA, Coordinate pointB)
        {
            if (pointA == null || pointB == null)
                throw new ArgumentNullException();

            var d1 = pointA.Latitude * (Math.PI / 180.0);
            var num1 = pointA.Longitude * (Math.PI / 180.0);
            var d2 = pointB.Latitude * (Math.PI / 180.0);
            var num2 = pointB.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return EARTH_RADIUS_KM * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))) / MILES_PER_KILOMETER;
        }
    }
}
