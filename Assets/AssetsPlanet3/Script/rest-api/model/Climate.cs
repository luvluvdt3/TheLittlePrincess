using System;
using System.Collections.Generic;

namespace Assets.planet3.rest_api.model
{
    /*
     {
       "latitude":52.5,
       "longitude":13.400009,
       "generationtime_ms":4.034996032714844,
       "utc_offset_seconds":0,
       "timezone":"GMT",
       "timezone_abbreviation":"GMT",
       "elevation":38.0,
       "daily_units":{
          "time":"iso8601",
          "temperature_2m_mean_HiRAM_SIT_HR":"°C",
          "temperature_2m_max_HiRAM_SIT_HR":"°C",
          "temperature_2m_min_HiRAM_SIT_HR":"°C",
          "precipitation_sum_HiRAM_SIT_HR":"mm",
          "rain_sum_HiRAM_SIT_HR":" mm",
          "snowfall_sum_HiRAM_SIT_HR":"cm",
          "temperature_2m_mean_EC_Earth3P_HR":"°C",
          "temperature_2m_max_EC_Earth3P_HR":"°C",
          "temperature_2m_min_EC_Earth3P_HR":"°C",
          "precipitation_sum_EC_Earth3P_HR":"mm",
          "rain_sum_EC_Earth3 P_HR":"mm",
          "snowfall_sum_EC_Earth3P_HR":"cm"
       },
       "daily":{
          "time":[
             "2000-01-01"
          ],
          "temperature_2m_mean_HiRAM_SIT_HR":[
             2.7,
             0.7
          ],
          "temperature_2m_max_HiRAM_SIT_HR":[
             7.1,
             2.7,
             0.6
          ],
          "temperature_2m_min_HiRAM_SIT_HR":[
             0.3,
             -1.7
          ],
          "precipitation_sum_HiRAM_SIT_HR":[
             5.18,
             1.46
          ],
          "snowfall_sum_HiRAM_SIT_HR":[
             0.00,
             0.00
          ],
          "temperature_2m_mean_EC_Earth3P_HR ":[
             3.4,
             -1.0
          ],
          "temperature_2m_max_EC_Earth3P_HR":[
             5.0,
             0.9
          ],
          "temperature_2m_min_EC_Earth3P_HR":[
             0.1,
             -2.7
          ],
          "precipitation_sum_EC_Earth3P_HR":[
             0.55,
             0.09
          ],
          "rain_sum_EC_Earth3P_HR":[
             0.55,
             0.0 2,
             0.03
          ],
          "snowfall_sum_EC_Earth3P_HR":[
             0.00,
             "...."
          ]
       }
    }
     */
    
    [Serializable]
    public class Daily
    {
        public List<int> time;
        public List<double> temperature_2m_mean_HiRAM_SIT_HR;
        public List<double> temperature_2m_max_HiRAM_SIT_HR;
        public List<double> temperature_2m_min_HiRAM_SIT_HR;
        public List<double> precipitation_sum_HiRAM_SIT_HR;
        public List<object> rain_sum_HiRAM_SIT_HR;
        public List<object> snowfall_sum_HiRAM_SIT_HR;
        public List<double> temperature_2m_mean_EC_Earth3P_HR;
        public List<double> temperature_2m_max_EC_Earth3P_HR;
        public List<double> temperature_2m_min_EC_Earth3P_HR;
        public List<double> precipitation_sum_EC_Earth3P_HR;
        public List<object> rain_sum_EC_Earth3P_HR;
        public List<object> snowfall_sum_EC_Earth3P_HR;
    }
    
    [Serializable]
    public class DailyUnits
    {
        public string time;
        public string temperature_2m_mean_HiRAM_SIT_HR;
        public string temperature_2m_max_HiRAM_SIT_HR;
        public string temperature_2m_min_HiRAM_SIT_HR;
        public string precipitation_sum_HiRAM_SIT_HR;
        public string rain_sum_HiRAM_SIT_HR;
        public string snowfall_sum_HiRAM_SIT_HR;
        public string temperature_2m_mean_EC_Earth3P_HR;
        public string temperature_2m_max_EC_Earth3P_HR;
        public string temperature_2m_min_EC_Earth3P_HR;
        public string precipitation_sum_EC_Earth3P_HR;
        public string rain_sum_EC_Earth3P_HR;
        public string snowfall_sum_EC_Earth3P_HR;
    }
    
    [Serializable]
    public class Climate
    {
        public double latitude;
        public double longitude;
        public double generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public double elevation;
        public DailyUnits daily_units;
        public Daily daily;
    }


}
