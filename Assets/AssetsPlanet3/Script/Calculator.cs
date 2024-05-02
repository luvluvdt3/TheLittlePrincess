using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NGeoNames;
using NGeoNames.Entities;
using planet3.rest_api.model;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    private const int CloseRadiusKm = 200;

    public Transform earth;

    private ExtendedGeoName[] _data;
    private ReverseGeoCode<ExtendedGeoName> _reverseGeocoder;

    private float EarthRadius => earth.GetComponent<Collider>().bounds.size.x / 2;

    private void Awake()
    {
        _data = GeoFileReader.ReadExtendedGeoNames("Assets/AssetsPlanet3/Script/res/cities15000.txt").ToArray();
        _reverseGeocoder = new ReverseGeoCode<ExtendedGeoName>(_data);
    }

    public Vector3 CalculatePositionFromLatAndLon(Coordinate coordinates, float distanceFromRadius)
    {
        // Convert to radians
        var latitude = coordinates.Latitude * Mathf.Deg2Rad;
        var longitude = coordinates.Longitude * Mathf.Deg2Rad;

        Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);

        var x = (EarthRadius + distanceFromRadius) * Mathf.Cos(latitude) * Mathf.Sin(-longitude);
        var y = (EarthRadius + distanceFromRadius) * Mathf.Sin(latitude);
        var z = (EarthRadius + distanceFromRadius) * Mathf.Cos(latitude) * Mathf.Cos(-longitude);

        Debug.Log("Calculated position: " + new Vector3(x, y, z));

        return new Vector3(x, y, z);
    }

    public Coordinate CalculateLatAndLonFromPosition(Vector3 position)
    {
        // Normalize position
        position /= EarthRadius;

        var longitude = Mathf.Atan2(position.z, position.x);
        var latitude = Mathf.Asin(position.y);

        latitude = latitude * Mathf.Rad2Deg;
        longitude = longitude * Mathf.Rad2Deg;

        // this bit is to flip and quarter rotate the longitude number to put it where you would expect
        longitude -= 90f;

        return new Coordinate(latitude, longitude);
    }

    [CanBeNull]
    private static Country GetCountry(ExtendedGeoName city)
    {
        return LoadData.Countries.FirstOrDefault(x => x.Value.Alpha2Code == city.CountryCode).Value;
    }

    private IEnumerable<ExtendedGeoName> GetCloseCities(Coordinate coordinates)
    {
        return _reverseGeocoder.RadialSearch(coordinates.Latitude, coordinates.Longitude, CloseRadiusKm);
    }

    public IEnumerable<Country> GetCloseCountries(Coordinate coordinates)
    {
        return GetCloseCities(coordinates)
            .Select(GetCountry)
            .Where(x => x != null)
            .Distinct();
    }
}