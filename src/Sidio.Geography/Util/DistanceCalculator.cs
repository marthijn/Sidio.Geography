using System.Diagnostics.CodeAnalysis;
using Sidio.Geography.Extensions;
using Sidio.Geography.Models;

namespace Sidio.Geography.Util;

internal static class DistanceCalculator
{
    public static Distance Haversine(GeoCoordinate coordinate1, GeoCoordinate coordinate2)
    {
        const double EarthRadiusInMeters = 6371000;
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static Distance Vincenty(GeoCoordinate coordinate1, GeoCoordinate coordinate2)
    {
        //// https://en.wikipedia.org/wiki/Vincenty%27s_formulae

        const double a = 6378137; // length of semi-major axis of the ellipsoid (radius at equator) in meters. WGS-84
        const double f = 1 / 298.257223563; // flattening of the ellipsoid. WGS-84
        const double b = (1 - f) * a; // length of semi-minor axis of the ellipsoid (radius at poles) in meters. WGS-84

        // latitude of the points
        var φ1 = coordinate1.Latitude.ToRadians();
        var φ2 = coordinate2.Latitude.ToRadians();

        // reduced latitude
        var U1 = Math.Atan((1 - f) * Math.Tan(φ1));
        var U2 = Math.Atan((1 - f) * Math.Tan(φ2));

        // longitude of the points
        var L1 = coordinate1.Longitude.ToRadians();
        var L2 = coordinate2.Longitude.ToRadians();
        var L = L2 - L1;

        // calculations
        var cosU1 = Math.Cos(U1);
        var cosU2 = Math.Cos(U2);
        var sinU1 = Math.Sin(U1);
        var sinU2 = Math.Sin(U2);

        var λ = L; // difference in longitude of the points on the auxiliary sphere;
        double σ; // angular separation between points

        // iteration variables
        double λp;
        double cosSqα;
        double sinσ;
        double cosσ;
        double cos2σm;

        const int MaxIteratons = 200;
        const double DesiredAccuracy = 1e-12;
        var i = 0;
        do
        {
            var sinλ = Math.Sin(λ);
            var cosλ = Math.Cos(λ);

            sinσ = Math.Sqrt(
                Math.Pow(cosU2 * sinλ, 2) +
                Math.Pow(cosU1 * sinU2 - sinU1 * cosU2 * cosλ, 2));

            if (sinσ == 0)
            {
                return new Distance(0);
            }

            cosσ = sinU1 * sinU2 + cosU1 * cosU2 * cosλ;
            σ = Math.Atan2(sinσ, cosσ);

            var sinα = (cosU1 * cosU2 * sinλ) / sinσ;
            cosSqα = 1 - Math.Pow(sinα, 2);
            cos2σm = cosσ - (2 * sinU1 * sinU2) / cosSqα;

            if (double.IsNaN(cos2σm))
            {
                cos2σm = 0; // equatorial line
            }

            var C = f / 16 * cosSqα * (4 + f * (4 - 3 * cosSqα));
            λp = λ;
            λ = L + (1 - C) * f * sinα *
                (σ + C * sinσ * (cos2σm + C * cosσ * (-1 + 2 * Math.Pow(cos2σm, 2))));
        }
        while(Math.Abs(λ - λp) > DesiredAccuracy && ++i < MaxIteratons);

        if (i >= MaxIteratons)
        {
            return new Distance(double.NaN);
        }

        var uSq = cosSqα * (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2);
        var A = 1 + (uSq / 16384) * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
        var B = (uSq / 1024) * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));
        var Δσ = B * sinσ * (cos2σm + (B / 4) * (cosσ * (-1 + 2 * Math.Pow(cos2σm, 2)) -
            (B / 6) * cos2σm * (-3 + 4 * Math.Pow(sinσ, 2)) * (-3 + 4 * Math.Pow(cos2σm, 2))));
        var s = b * A * (σ - Δσ);

        return new Distance(s);
    }
}