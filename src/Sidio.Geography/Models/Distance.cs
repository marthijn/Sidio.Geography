namespace Sidio.Geography.Models;

/// <summary>
/// This class represents a distance.
/// </summary>
public sealed record Distance
{
    internal Distance(double meters)
    {
        if (meters < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(meters), "Distance cannot be negative.");
        }

        Meters = meters;
    }

    /// <summary>
    /// Gets the distance in meters.
    /// </summary>
    public double Meters { get; }

    /// <summary>
    /// Gets the distance in miles.
    /// </summary>
    public double Miles => Meters / 1609.344;

    /// <summary>
    /// Gets the distance in nautical miles.
    /// </summary>
    public double NauticalMiles => Meters / 1852;
}