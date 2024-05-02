using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Assets.planet3.rest_api.model;
using planet3.rest_api.model;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherAPI : MonoBehaviour
{
    /*
    Url example:
    
        https://api.open-meteo.com/v1/forecast?latitude=31.2323437,39.9057136,23.7644025,30.6598628,28.6517178,41.0091982,24.8546842,23.1301964,39.084802,35.6812665,31.3016935,55.7504461,22.5445741,30.5951051,19.0785451,-23.5506507,31.5656822,12.9767936,-6.175247,6.4550575,30.2489634,29.5656729,38.0429742,-12.0621065,23.0239788,31.8228094,32.0615513,27.9963899,36.7071591,36.583333,30.0443879,43.8844201
        &longitude=121.4691024,116.3912972,90.389015,104.0633717,77.2219388,28.9662187,67.0207055,113.2592945,117.1959904,139.757653,120.5810725,37.6174943,114.0545429,114.2999353,72.878176,-46.6333824,74.3141829,77.590082,106.8270488,3.3941795,120.2052342,106.5479189,114.5088385,-77.0365256,113.1159558,117.2218033,118.7915619,120.695345,119.1557441,114.483333,31.2357257,125.3180998
        &hourly=temperature_2m,relative_humidity_2m,dew_point_2m,apparent_temperature,precipitation_probability,precipitation,rain,showers,snowfall,snow_depth,weather_code,pressure_msl,surface_pressure,cloud_cover,cloud_cover_low,cloud_cover_mid,cloud_cover_high,visibility,evapotranspiration,vapour_pressure_deficit,wind_speed_10m
    */

    public static Action<Dictionary<Coordinate, Country>, CurrentWeather> OnWeatherDataReceived;
    public static Action<Country, Climate> OnClimateDataReceived;

    public void GetAllVisibleCountriesWeatherData(Dictionary<Coordinate, Country> coordinates)
    {
        Debug.Log("Fetching weather data for " + coordinates.Count + " countries");

        var countriesCoordinates = coordinates.Keys.ToArray();

        string url = OpenMeteoUrlBuilder.Forecast()
            .AddCoordinates(countriesCoordinates)
            .AddCurrentParameters(OpenMeteoUrlBuilder.DEFAULT_COUNTRIES_PARAMETERS)
            .AddForeCastDays(1)
            .Build();

        StartCoroutine(SendRequest(url, WeatherDataPostProcessing, coordinates));
    }    
    
    public void GetClimateChangeForCountry(Country country)
    {
        Debug.Log("Fetching climate change data for " + country.Name);

        string url = OpenMeteoUrlBuilder.Climate()
            .AddCoordinates(country.Coordinates)
            .AddBeginAndEndDateForClimate(new DateTime(2000, 1, 1), new DateTime(2024, 4, 1))
            .AddModels(OpenMeteoUrlBuilder.DEFAULT_MODELS)
            .AddDailyParameters(OpenMeteoUrlBuilder.DEFAULT_CLIMATE_PARAMETERS)
            .Build();

        Debug.Log("URL: " + url);

        StartCoroutine(SendRequest(url, ClimateDataPostProcessing, new Dictionary<Coordinate, Country> { { country.Coordinates, country } }));
    }

    private IEnumerator SendRequest(string url, Action<Dictionary<Coordinate, Country>, string> onResponseReceived, Dictionary<Coordinate, Country> coordinates)
    {
        using var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            onResponseReceived(coordinates, www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    private void ClimateDataPostProcessing(Dictionary<Coordinate, Country> dictionary, string data)
    {
        var locationWeather = JsonUtility.FromJson<Climate>(data);
        OnClimateDataReceived?.Invoke(dictionary.Values.First(), locationWeather);
    }

    private void WeatherDataPostProcessing(Dictionary<Coordinate, Country> coordinates, string data)
    {
        var locationWeatherList = JsonUtility.FromJson<CurrentWeather>("{\"weatherDatas\":" + data + "}");
        OnWeatherDataReceived?.Invoke(coordinates, locationWeatherList);
    }
}

public class OpenMeteoUrlBuilder
{
    private static readonly string FORECAST_LINK = "https://api.open-meteo.com/v1/forecast?";
    private static readonly string CLIMATE_LINK = "https://climate-api.open-meteo.com/v1/climate?";

    public static readonly string[] DEFAULT_MODELS =
    {
        "HiRAM_SIT_HR",
        "EC_Earth3P_HR"
    };

    public static readonly string[] DEFAULT_DETAILED_HOURLY_PARAMETERS =
    {
        "temperature_2m",
        "relative_humidity_2m",
        "dew_point_2m",
        "apparent_temperature",
        "precipitation_probability",
        "precipitation",
        "rain",
        "showers",
        "snowfall",
        "snow_depth",
        "weather_code",
        "pressure_msl",
        "surface_pressure",
        "cloud_cover",
        "cloud_cover_low",
        "cloud_cover_mid",
        "cloud_cover_high",
        "visibility",
        "evapotranspiration",
        "vapour_pressure_deficit",
        "wind_speed_10m"
    };

    public static readonly string[] DEFAULT_COUNTRIES_PARAMETERS =
    {
        "temperature_2m",
        "relative_humidity_2m",
        "apparent_temperature",
        "is_day",
        "precipitation",
        "rain",
        "showers",
        "snowfall",
        "weather_code",
        "cloud_cover",
        "pressure_msl",
        "surface_pressure",
        "wind_speed_10m",
        "wind_direction_10m",
        "wind_gusts_10m"
    };

    public static readonly string[] DEFAULT_CLIMATE_PARAMETERS =
    {
        "temperature_2m_mean",
        "temperature_2m_max",
        "temperature_2m_min",
        "precipitation_sum",
        "rain_sum",
        "snowfall_sum"
    };

    private readonly StringBuilder _urlBuilder;

    private OpenMeteoUrlBuilder(string call)
    {
        _urlBuilder = new StringBuilder().Append(call);
    }

    public OpenMeteoUrlBuilder AddBeginAndEndDateForClimate(DateTime begin, DateTime end)
    {
        _urlBuilder.Append("&start_date=");
        _urlBuilder.Append(begin.ToString("yyyy-MM-dd"));

        _urlBuilder.Append("&end_date=");
        _urlBuilder.Append(end.ToString("yyyy-MM-dd"));
        Debug.Log("URL: " + _urlBuilder);

        return this;
    }
    public OpenMeteoUrlBuilder AddCoordinates(params Coordinate[] coordinates)
    {
        Debug.Log("Adding coordinates: " + coordinates.Length);
        if (coordinates.Length > 0)
        {
            var latitudeValues = coordinates.Select(c => c.Latitude.ToString(CultureInfo.InvariantCulture)).ToArray();
            var longitudeValues = coordinates.Select(c => c.Longitude.ToString(CultureInfo.InvariantCulture)).ToArray();

            _urlBuilder.Append("&latitude=");
            _urlBuilder.Append(string.Join(",", latitudeValues));

            _urlBuilder.Append("&longitude=");
            _urlBuilder.Append(string.Join(",", longitudeValues));
        }

        Debug.Log("URL: " + _urlBuilder);

        return this;
    }

    public OpenMeteoUrlBuilder AddDailyParameters(params string[] parameters)
    {
        if (parameters.Length > 0)
        {
            _urlBuilder.Append("&daily=");
            _urlBuilder.Append(string.Join(",", parameters));
        }
        Debug.Log("URL: " + _urlBuilder);

        return this;
    }

    public OpenMeteoUrlBuilder AddHourlyParameters(params string[] parameters)
    {
        if (parameters.Length > 0)
        {
            _urlBuilder.Append("&hourly=");
            _urlBuilder.Append(string.Join(",", parameters));
        }
        Debug.Log("URL: " + _urlBuilder);

        return this;
    }

    public OpenMeteoUrlBuilder AddCurrentParameters(params string[] parameters)
    {
        if (parameters.Length > 0)
        {
            _urlBuilder.Append("&current=");
            _urlBuilder.Append(string.Join(",", parameters));
        }
        Debug.Log("URL: " + _urlBuilder);

        return this;
    }

    public OpenMeteoUrlBuilder AddBeginAndEndDate(DateTime begin, DateTime end)
    {
        _urlBuilder.Append("&start=");
        _urlBuilder.Append(begin.ToString("yyyy-MM-dd"));

        _urlBuilder.Append("&end=");
        _urlBuilder.Append(end.ToString("yyyy-MM-dd"));
        Debug.Log("URL: " + _urlBuilder);

        return this;
    }

    public OpenMeteoUrlBuilder AddModels(params string[] models)
    {
        //HiRAM_SIT_HR,EC_Earth3P_HR
        if (models.Length > 0)
        {
            _urlBuilder.Append("&models=");
            _urlBuilder.Append(string.Join(",", models));
        }
        Debug.Log("URL: " + _urlBuilder);
        return this;
    }

    public OpenMeteoUrlBuilder AddForeCastDays(int days)
    {
        _urlBuilder.Append("&forecast_days=");
        _urlBuilder.Append(days);
        Debug.Log("URL: " + _urlBuilder);

        return this;
    }

    public string Build()
    {
        return _urlBuilder.Append("&timeformat=unixtime").ToString();
    }

    public static OpenMeteoUrlBuilder Forecast()
    {
        return new OpenMeteoUrlBuilder(FORECAST_LINK);
    }
    
    public static OpenMeteoUrlBuilder Climate()
    {
        return new OpenMeteoUrlBuilder(CLIMATE_LINK);
    }
}