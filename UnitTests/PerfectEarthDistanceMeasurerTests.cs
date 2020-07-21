using DistanceMeasurerService.Models;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [TestFixture]
        public class PerfectEarthDistanceMeasurerTests
        {
            /// <summary>
            /// Test is distance is always positive
            /// </summary>
            /// <param name="sirenaPricingRs"></param>
            /// <param name="numberOfSegmentsExp">Количество сегментов. В ситуации с OW оно же равно количеству Flight'ов</param>
            [Test]
            [TestCase(50, 50, 50, 50)]
            [TestCase(50, 50, 50, -50)]
            [TestCase(50, 50, -50, 50)]
            [TestCase(50, 50, -50, -50)]
            [TestCase(50, -50, 50, 50)]
            [TestCase(50, -50, 50, -50)]
            [TestCase(50, -50, -50, 50)]
            [TestCase(50, -50, -50, -50)]
            [TestCase(-50, 50, 50, 50)]
            [TestCase(-50, 50, 50, -50)]
            [TestCase(-50, 50, -50, 50)]
            [TestCase(-50, 50, -50, -50)]
            [TestCase(-50, -50, 50, 50)]
            [TestCase(-50, -50, 50, -50)]
            [TestCase(-50, -50, -50, 50)]
            [TestCase(-50, -50, -50, -50)]
            public void GetDataListSegmentsCount(double lon1, double lat1, double lon2, double lat2)
            {
                // Arrange   
                var measurer = new PerfectSphereDistanceMeasurer();
                Coordinate c1 = new Coordinate() { Latitude = lat1, Longitude = lon1 };
                Coordinate c2 = new Coordinate() { Latitude = lat2, Longitude = lon2 };

                // Act
                double distance = measurer.CalculateDistanceMiles(c1, c2);

                // Assert
                Assert.GreaterOrEqual(distance, 0, "Distance is not positive");
            }
        }
    }
}