using Assets.planet3.rest_api.model;
using planet3.rest_api.model;
using System;
using UnityEngine;
using XCharts.Runtime;

namespace AssetsPlanet3.Script.climate
{
    public class GraphController : MonoBehaviour
    {
        private LineChart _chart;

        void Awake()
        {
            _chart = gameObject.GetComponent<LineChart>();
            WeatherAPI.OnClimateDataReceived += OnClimateDataReceived;
        }

        private void OnClimateDataReceived(Country country, Climate climate)
        {
            Debug.Log("Climate Data Received");
            _chart.RemoveData();
            _chart.AddSerie<Line>("Mean Temperature");
            _chart.AddSerie<Line>("Min Temperature");
            _chart.AddSerie<Line>("Maximum Temperature");
            /*
        _chart.AddSerie<Line>("Precipitation");
        _chart.AddSerie<Line>("Rain");
        _chart.AddSerie<Line>("Snow");
        */

            var xAxis = _chart.EnsureChartComponent<XAxis>();
            xAxis.splitNumber = 25;
            xAxis.boundaryGap = true;
            xAxis.type = Axis.AxisType.Category;

            var yAxis = _chart.EnsureChartComponent<YAxis>();
            yAxis.type = Axis.AxisType.Value;

            var values = climate.daily;
            int time_previous = -1; // Initialize previous year to an invalid value
            int count = 0;
            double meanTemperatureSum = 0.0;
            double minTemperatureSum = 0.0;
            double maxTemperatureSum = 0.0;

            for (int i = 0; i < values.time.Count; i++)
            {
                var time = values.time[i];
                DateTimeOffset dateTimeFromSecondsTemp = DateTimeOffset.FromUnixTimeSeconds(time);
                int year = dateTimeFromSecondsTemp.Year;

                var meanTemperature = values.temperature_2m_mean_HiRAM_SIT_HR[i];
                var minTemperature = values.temperature_2m_min_HiRAM_SIT_HR[i];
                var maxTemperature = values.temperature_2m_max_HiRAM_SIT_HR[i];

                if (time_previous != year)
                {
                    // Calculate average for the previous year and add it to the chart
                    if (count > 0)
                    {
                        double meanTemperatureAvg = meanTemperatureSum / count;
                        double minTemperatureAvg = minTemperatureSum / count;
                        double maxTemperatureAvg = maxTemperatureSum / count;

                        _chart.AddXAxisData(time_previous.ToString()[2..]);
                        _chart.AddData(0, meanTemperatureAvg);
                        _chart.AddData(1, minTemperatureAvg);
                        _chart.AddData(2, maxTemperatureAvg);

                        meanTemperatureSum = 0f;
                        minTemperatureSum = 0f;
                        maxTemperatureSum = 0f;
                        count = 0;
                    }

                    time_previous = year;
                }

                // Accumulate data for the current year
                meanTemperatureSum += meanTemperature;
                minTemperatureSum += minTemperature;
                maxTemperatureSum += maxTemperature;
                count++;
            }
        }


            // Update is called once per frame
        void Update()
        {
        
        }
    }
}
