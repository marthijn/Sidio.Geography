using Sidio.Geography.Extensions;

namespace Sidio.Geography.Tests.Extensions;

public sealed class DoubleExtensionsTests
{
    [Fact]
    public void ToRadians_ShouldConvertDegreesToRadians()
    {
        // Arrange
        const double Degrees = 180.0;

        // Act
        var radians = Degrees.ToRadians();

        // Assert
        radians.Should().BeApproximately(Math.PI, 0.0001);
    }
}