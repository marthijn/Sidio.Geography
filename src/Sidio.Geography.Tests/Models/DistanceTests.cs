﻿using Sidio.Geography.Models;

namespace Sidio.Geography.Tests.Models;

public sealed class DistanceTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(double.NaN)]
    public void Constructor_ReturnsInstance(double input)
    {
        // Act
        var result = new Distance(input);

        // Assert
        result.Should().NotBeNull();
        result.Meters.Should().Be(input);
    }

    [Fact]
    public void Constructor_WhenMetersIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const double NegativeMeters = -1;

        // Act
        Action act = () => _ = new Distance(NegativeMeters);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.000621371192237334)]
    [InlineData(1000, 0.621371192237334)]
    public void ToMiles_ReturnsMiles(double meters, double expectedMiles)
    {
        // Arrange
        var distance = new Distance(meters);

        // Act
        var result = distance.ToMiles();

        // Assert
        result.Should().BeApproximately(expectedMiles, 0.0001);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.000539956803)]
    [InlineData(1000, 0.539956803)]
    public void ToNauticalMiles_ReturnsNauticalMiles(double meters, double expectedNauticalMiles)
    {
        // Arrange
        var distance = new Distance(meters);

        // Act
        var result = distance.ToNauticalMiles();

        // Assert
        result.Should().BeApproximately(expectedNauticalMiles, 0.0001);
    }
}