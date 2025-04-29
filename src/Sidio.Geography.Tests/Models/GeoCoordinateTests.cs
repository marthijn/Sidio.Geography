using Sidio.Geography.Models;

namespace Sidio.Geography.Tests.Models;

public sealed class GeoCoordinateTests
{
    [Theory]
    [InlineData(-90.1, 0.0)]
    [InlineData(90.1, 0.0)]
    [InlineData(0, -180.1)]
    [InlineData(0, 180.1)]
    public void Construct_WithInvalidCoordinates_ThrowsArgumentOutOfRangeException(double latitude, double longitude)
    {
        // Act
        var action = () => new GeoCoordinate(latitude, longitude);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}