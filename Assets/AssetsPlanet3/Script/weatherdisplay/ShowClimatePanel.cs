using Assets.planet3.rest_api.model;
using planet3.rest_api.model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace AssetsPlanet3.Script.weatherdisplay
{
    public class ShowClimatePanel : MonoBehaviour, IPointerClickHandler
    {
        public GameObject climateBar;
        public WeatherAPI weatherAPI;
        
        public Country Country { get; set; }
        
        void Start()
        {
            climateBar.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Country == null)
                return;
            
            climateBar.SetActive(true);
            weatherAPI.GetClimateChangeForCountry(Country);
        }
    }
}
