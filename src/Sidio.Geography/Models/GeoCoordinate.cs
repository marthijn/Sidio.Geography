using Sidio.Geography.Util;

namespace Sidio.Geography.Models;

/// <summary>
/// This class represents a geographical coordinate.
/// </summary>
public sealed record GeoCoordinate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeoCoordinate"/> class.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    public GeoCoordinate(double latitude, double longitude)
    {
        if (latitude is < -90 or > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");
        }

        if (longitude is < -180 or > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");
        }

        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Gets the latitude of the coordinate.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Gets the longitude of the coordinate.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Calculates the distance to another geographical coordinate.
    /// </summary>
    /// <param name="target">The target coordinate.</param>
    /// <param name="formula">The distance formula.</param>
    /// <returns>A <see cref="Distance"/>.</returns>
    public Distance DistanceTo(GeoCoordinate target, DistanceFormula formula = DistanceFormula.Haversine)
    {
        return formula switch
        {
            DistanceFormula.Haversine => DistanceCalculator.Haversine(this, target),
            DistanceFormula.Vincenty => DistanceCalculator.Vincenty(this, target),
            _ => throw new ArgumentOutOfRangeException(nameof(formula), formula, null)
        };
    }
}