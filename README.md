# Sidio.Geography
A lightweight C# library for calculating geographic distances between coordinates.

[![NuGet Version](https://img.shields.io/nuget/v/Sidio.Geography)](https://www.nuget.org/packages/Sidio.Geography/)
[![build](https://github.com/marthijn/Sidio.Geography/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.Geography/actions/workflows/build.yml)
[![Coverage Status](https://coveralls.io/repos/github/marthijn/Sidio.Geography/badge.svg?branch=main)](https://coveralls.io/github/marthijn/Sidio.Geography?branch=main)

# Usage

## Calculate distance between two coordinates
```csharp
var coordinate1 = new GeoCoordinate(lat1, lon1);
var coordinate2 = new GeoCoordinate(lat2, lon2);

// Haversine formula
var distanceHaversine = coordinate1.DistanceTo(coordinate2);

// Vincenty formula
var distanceVincenty = coordinate1.DistanceTo(coordinate2, DistanceFormula.Vincenty);
```