using Assets.planet3.rest_api.model;
using System;

namespace planet3.rest_api.model
{
    [Serializable]
    public class CurrentUnits : Units
    {
        public string interval;
        public string is_day;
        public string wind_direction_10m;
        public string wind_gusts_10m;
    }

    [Serializable]
    public class Current
    {
        public long time;
        public int interval;
        public double temperature_2m;
        public int relative_humidity_2m;
        public double apparent_temperature;
        public bool is_day;
        public double precipitation;
        public double rain;
        public double showers;
        public double snowfall;
        public int weather_code;
        public int cloud_cover;
        public double pressure_msl;
        public double surface_pressure;
        public double wind_speed_10m;
        public int wind_direction_10m;
        public double wind_gusts_10m;
    }

    [Serializable]
    public class CurrentWeatherData : WeatherData
    {
        public double latitude;
        public double longitude;
        public CurrentUnits current_units;
        public Current current;
    }

    [Serializable]
    public class CurrentWeather
    {
        public CurrentWeatherData[] weatherDatas;
    }
}
