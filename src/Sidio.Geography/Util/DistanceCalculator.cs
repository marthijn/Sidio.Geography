using Sidio.Geography.Extensions;
using Sidio.Geography.Models;

namespace Sidio.Geography.Util;

internal static class DistanceCalculator
{
    private const double EarthRadiusInMeters = 6371000;

    public static Distance Calculate(GeoCoordinate coordinate1, GeoCoordinate coordinate2)
    {
        var radiansLat1 = coordinate1.Latitude.ToRadians();
        var radiansLat2 = coordinate2.Latitude.ToRadians();
        var radiansLat = (coordinate2.Latitude - coordinate1.Latitude).ToRadians();
        var radiansLon = (coordinate2.Longitude - coordinate1.Longitude).ToRadians();

        var a = Math.Sin(radiansLat / 2) * Math.Sin(radiansLat / 2) +
                   Math.Cos(radiansLat1) * Math.Cos(radiansLat2) *
                   Math.Sin(radiansLon / 2) * Math.Sin(radiansLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var distanceInMeters = EarthRadiusInMeters * c;
        return new Distance(distanceInMeters);
    }
}