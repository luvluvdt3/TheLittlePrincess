using UnityEngine;

public class WeatherConstantTextures : MonoBehaviour
{
    public RenderTexture clear_day_texture;
    public RenderTexture clear_night_texture;

    public RenderTexture drizzle_day_texture;
    public RenderTexture drizzle_night_texture;

    public RenderTexture fog_day_texture;
    public RenderTexture fog_night_texture;

    public RenderTexture freezing_drizzle_day_texture;
    public RenderTexture freezing_drizzle_night_texture;

    public RenderTexture freezing_rain_day_texture;
    public RenderTexture freezing_rain_night_texture;

    public RenderTexture hail_day_texture;
    public RenderTexture hail_night_texture;

    public RenderTexture heavy_snow_day_texture;
    public RenderTexture heavy_snow_night_texture;

    public RenderTexture light_drizzle_day_texture;
    public RenderTexture light_drizzle_night_texture;

    public RenderTexture light_snow_day_texture;
    public RenderTexture light_snow_night_texture;

    public RenderTexture medium_drizzle_day_texture;
    public RenderTexture medium_drizzle_night_texture;

    public RenderTexture overcast_day_texture;
    public RenderTexture overcast_night_texture;

    public RenderTexture partly_cloudy_day_texture;
    public RenderTexture partly_cloudy_night_texture;

    public RenderTexture rain_day_texture;
    public RenderTexture rain_night_texture;

    public RenderTexture rime_fog_day_texture;
    public RenderTexture rime_fog_night_texture;

    public RenderTexture thunderstorm_rain_day_texture;
    public RenderTexture thunderstorm_rain_night_texture;

    public RenderTexture thunderstorm_snow_day_texture;
    public RenderTexture thunderstorm_snow_night_texture;


    void Start()
    {
    }

    public RenderTexture GetTextureByName(string name)
    {
        return name switch
        {
            "clear_day" => clear_day_texture,
            "clear_night" => clear_night_texture,
            "drizzle_day" => drizzle_day_texture,
            "drizzle_night" => drizzle_night_texture,
            "fog_day" => fog_day_texture,
            "fog_night" => fog_night_texture,
            "freezing_drizzle_day" => freezing_drizzle_day_texture,
            "freezing_drizzle_night" => freezing_drizzle_night_texture,
            "freezing_rain_day" => freezing_rain_day_texture,
            "freezing_rain_night" => freezing_rain_night_texture,
            "hail_day" => hail_day_texture,
            "hail_night" => hail_night_texture,
            "heavy_snow_day" => heavy_snow_day_texture,
            "heavy_snow_night" => heavy_snow_night_texture,
            "light_drizzle_day" => light_drizzle_day_texture,
            "light_drizzle_night" => light_drizzle_night_texture,
            "light_snow_day" => light_snow_day_texture,
            "light_snow_night" => light_snow_night_texture,
            "medium_drizzle_day" => medium_drizzle_day_texture,
            "medium_drizzle_night" => medium_drizzle_night_texture,
            "overcast_day" => overcast_day_texture,
            "overcast_night" => overcast_night_texture,
            "partly_cloudy_day" => partly_cloudy_day_texture,
            "partly_cloudy_night" => partly_cloudy_night_texture,
            "rain_day" => rain_day_texture,
            "rain_night" => rain_night_texture,
            "rime_fog_day" => rime_fog_day_texture,
            "rime_fog_night" => rime_fog_night_texture,
            "thunderstorm_rain_day" => thunderstorm_rain_day_texture,
            "thunderstorm_rain_night" => thunderstorm_rain_night_texture,
            "thunderstorm_snow_day" => thunderstorm_snow_day_texture,
            "thunderstorm_snow_night" => thunderstorm_snow_night_texture,
            _ => clear_day_texture,
        };
    }
}
