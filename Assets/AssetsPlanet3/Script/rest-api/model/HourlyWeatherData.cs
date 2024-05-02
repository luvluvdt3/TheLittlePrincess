using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Assets.planet3.rest_api.model;

namespace planet3.rest_api.model
{
    [Serializable]
    public class HourlyUnits : Units
    {
        public string dew_point_2m;
        public string snow_depth;
        public string cloud_cover_low;
        public string cloud_cover_mid;
        public string cloud_cover_high;
        public string visibility;
        public string evapotranspiration;
        public string vapour_pressure_deficit;
    }

    [Serializable]
    public class HourlyData
    {
        public long[] time;
        public double[] temperature_2m;
        public int[] relative_humidity_2m;
        public double[] dew_point_2m;
        public double[] apparent_temperature;
        public int[] precipitation_probability;
        public double[] precipitation;
        public double[] rain;
        public double[] showers;
        public double[] snowfall;
        public double[] snow_depth;
        public int[] weather_code;
        public double[] pressure_msl;
        public double[] surface_pressure;
        public int[] cloud_cover;
        public int[] cloud_cover_low;
        public int[] cloud_cover_mid;
        public int[] cloud_cover_high;
        public double[] visibility;
        public double[] evapotranspiration;
        public double[] vapour_pressure_deficit;
        public double[] wind_speed_10m;
    }

    [Serializable]
    public class HourlyWeatherData : WeatherData
    {
        public double[] latitude { get; set; }
        public double[] longitude { get; set; }
        public HourlyUnits hourly_units;
        public HourlyData hourly;
    }
}

