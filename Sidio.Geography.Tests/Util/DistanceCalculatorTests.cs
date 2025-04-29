using Sidio.Geography.Models;

namespace Sidio.Geography.Tests.Util;

public sealed class DistanceCalculatorTests
{
    [Theory]
    [InlineData(52.3730796, 4.8924534, 52.0907006, 5.1215634, 35060)]
    public void Calculate_ReturnsDistance(double lat1, double lon1, double lat2, double lon2, double expectedInMeters)
    {
        // Arrange
        var coordinate1 = new GeoCoordinate(lat1, lon1);
        var coordinate2 = new GeoCoordinate(lat2, lon2);

        // Act
        var result = coordinate1.DistanceTo(coordinate2);

        // Assert
        result.Meters.Should().BeApproximately(expectedInMeters, 5);
    }
}