using System;

namespace Assets.planet3.rest_api.model
{
    [Serializable]
    public class Units
    {
        public string time;
        public string temperature_2m;
        public string relative_humidity_2m;
        public string apparent_temperature;
        public string precipitation;
        public string rain;
        public string showers;
        public string snowfall;
        public string weather_code;
        public string pressure_msl;
        public string surface_pressure;
        public string cloud_cover;
        public string wind_speed_10m;
    }

    [Serializable]
    public class WeatherData
    {
        public double generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public double elevation;
    }

}
