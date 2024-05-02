
using planet3.rest_api.model;
using System;
using Assets.planet3.rest_api.model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowWeatherPanel : MonoBehaviour, IPointerClickHandler
{
    private CurrentWeatherData weatherData;
    private WeatherCondition weatherCondition;
    private Country country;

    private Color initialColor;

    public GameObject weatherSideDisplay;
    private GameObject humadity;
    public GameObject elementPanel;


    public void SetWeatherData(CurrentWeatherData data)
    {
        weatherData = data;
    }

    public void SetWeatherCondition(WeatherCondition data)
    {
        weatherCondition = data;
    }

    public void SetCountry(Country data)
    {
        country = data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (weatherData == null || weatherCondition == null || country == null)
            return;
        elementPanel.SetActive(false);
        weatherSideDisplay.SetActive(true);
        Image panelImage = weatherSideDisplay.GetComponent<Image>();

        panelImage.color = !weatherData.current.is_day ? new Color(0.58f, 0.78f, 0.99f, 207f / 255f) : initialColor;

        weatherSideDisplay.GetComponentInChildren<VideoPlayer>().url = "file://" + weatherCondition.AnimationLink;
        weatherSideDisplay.GetComponentInChildren<VideoPlayer>().Play();

        RawImage rawImage = weatherSideDisplay.GetComponentInChildren<RawImage>();
        rawImage.GetComponentsInChildren<Text>()[0].text = weatherCondition.Description;
        rawImage.GetComponentsInChildren<Text>()[1].text = weatherData.current.temperature_2m.ToString() +
                                                           weatherData.current_units.temperature_2m;
        rawImage.GetComponentsInChildren<Text>()[2].text = weatherData.current.apparent_temperature.ToString() +
                                                           weatherData.current_units.apparent_temperature;
        rawImage.GetComponentsInChildren<Text>()[3].text = DateTimeOffset
            .FromUnixTimeMilliseconds(weatherData.current.time).DateTime.ToString("dd/MM/yy,hh:mm tt");

        weatherSideDisplay.GetComponentsInChildren<Text>()[0].text = country.Name;
        weatherSideDisplay.GetComponentsInChildren<Text>()[1].text = country.Coordinates.Latitude.ToString() + ":" +
                                                                     country.Coordinates.Longitude.ToString();

        GameObject humidityDisplay = weatherSideDisplay.transform.Find("humidity").gameObject;
        humidityDisplay.GetComponentsInChildren<Text>()[0].text = weatherData.current.relative_humidity_2m.ToString() +
                                                                  weatherData.current_units.relative_humidity_2m;

        GameObject precipitationDisplay = weatherSideDisplay.transform.Find("precipitation").gameObject;
        precipitationDisplay.GetComponentsInChildren<Text>()[0].text = weatherData.current.precipitation.ToString() +
                                                                       weatherData.current_units.precipitation;
        precipitationDisplay.GetComponentsInChildren<Text>()[1].text =
            weatherData.current.rain.ToString() + weatherData.current_units.rain;
        precipitationDisplay.GetComponentsInChildren<Text>()[2].text =
            weatherData.current.showers.ToString() + weatherData.current_units.showers;
        precipitationDisplay.GetComponentsInChildren<Text>()[3].text =
            weatherData.current.snowfall.ToString() + weatherData.current_units.snowfall;


        weatherSideDisplay.GetComponentsInChildren<Text>()[2].text = weatherData.current.cloud_cover.ToString() +
                                                                     weatherData.current_units.cloud_cover.ToString();

        GameObject pressureDisplay = weatherSideDisplay.transform.Find("pressure").gameObject;
        pressureDisplay.GetComponentsInChildren<Text>()[0].text =
            weatherData.current.pressure_msl.ToString() + weatherData.current_units.pressure_msl;
        pressureDisplay.GetComponentsInChildren<Text>()[1].text = weatherData.current.surface_pressure.ToString() +
                                                                  weatherData.current_units.surface_pressure;

        GameObject windDisplay = weatherSideDisplay.transform.Find("wind").gameObject;
        windDisplay.GetComponentsInChildren<Text>()[0].text = weatherData.current.wind_speed_10m.ToString() +
                                                              weatherData.current_units.wind_speed_10m;
        windDisplay.GetComponentsInChildren<Text>()[1].text = weatherData.current.wind_gusts_10m.ToString() +
                                                              weatherData.current_units.wind_gusts_10m;
    }

    void Start()
    {
        weatherSideDisplay.SetActive(false);
        initialColor = weatherSideDisplay.GetComponent<Image>().color;
    }
}
public class WindDirection
{
    public const string WIND_ICONS_PATH = "/planet3/plugins/icons/wind_direction/";

    public string Description { get; }

    public string Icon { get; }

    public string Abbreviation { get; }

    public string IconPath { get => Application.dataPath + WIND_ICONS_PATH + Icon + ".png"; }

    public WindDirection(string description, string icon, string abbreviation)
    {
        Description = description;
        Icon = icon;
        Abbreviation = abbreviation;
    }

    public static WindDirection GetWindDirectionFromDegrees(int degree)
    {
        if((degree <= 11.25 && degree >= 0) || degree <= 360 && degree >= 348.75)
        {
            return new WindDirection("North", "direction_north", "N");
        }
        else if(degree <= 33.75 && degree >= 11.25)
        {
            return new WindDirection("North North East", "direction_nne", "NNE");
        }
        else if(degree <= 56.25 && degree >= 33.75)
        {
            return new WindDirection("North East", "direction_ne", "NE");
        }
        else if(degree <= 78.75 && degree >= 56.25)
        {
            return new WindDirection("East North East", "direction_ene", "ENE");
        }
        else if(degree <= 101.25 && degree >= 78.75)
        {
            return new WindDirection("East", "direction_east", "E");
        }
        else if(degree <= 123.75 && degree >= 101.25)
        {
            return new WindDirection("East South East", "direction_ese", "ESE");
        }
        else if(degree <= 146.25 && degree >= 123.75)
        {
            return new WindDirection("South East", "direction_se", "SE");
        }
        else if(degree <= 168.75 && degree >= 146.25)
        {
            return new WindDirection("South South East", "direction_sse", "SSE");
        }
        else if(degree <= 191.25 && degree >= 168.75)
        {
            return new WindDirection("South", "direction_south", "S");
        }
        else if(degree <= 213.75 && degree >= 191.25)
        {
            return new WindDirection("South South West", "direction_ssw", "SSW");
        }
        else if(degree <= 236.25 && degree >= 213.75)
        {
            return new WindDirection("South West", "direction_sw", "SW");
        }
        else if(degree <= 258.75 && degree >= 236.25)
        {
            return new WindDirection("West South West", "direction_wsw", "WSW");
        }
        else if(degree <= 281.25 && degree >= 258.75)
        {
            return new WindDirection("West", "direction_west", "W");
        }
        else if(degree <= 303.75 && degree >= 281.25)
        {
            return new WindDirection("West North West", "direction_wnw", "WNW");
        }
        else if(degree <= 326.25 && degree >= 303.75)
        {
            return new WindDirection("North West", "direction_nw", "NW");
        }
        else if(degree <= 348.75 && degree >= 326.25)
        {
            return new WindDirection("North North West", "direction_nnw", "NNW");
        }
        else
        {
            return new WindDirection("Unknown", "U", "U");
        }
    }
}

