namespace Sidio.Geography.Models;

/// <summary>
/// The formula used to calculate the distance between two geographic coordinates.
/// </summary>
public enum DistanceFormula
{
    /// <summary>
    /// The haversine formula determines the great-circle distance between two points on a sphere given their
    /// longitudes and latitudes.
    /// </summary>
    Haversine,

    /// <summary>
    /// Vincenty's formulae are two related iterative methods used in geodesy to calculate the distance between
    /// two points on the surface of a spheroid.
    /// <remarks>Vincenty is more accurate, but also more computationally intensive than <see cref="Haversine"/>.</remarks>
    /// </summary>
    Vincenty
}