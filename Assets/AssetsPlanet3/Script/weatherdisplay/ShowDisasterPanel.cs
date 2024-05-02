using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace AssetsPlanet3.Script.weatherdisplay
{
    public class ShowDisasterPanel : MonoBehaviour, IPointerClickHandler
    {
        public Disaster Disaster { get; set; }
        public GameObject disasterSideBar;
        public RawImage display;
        public GameObject elementPanel;
        
        void Start()
        {
            disasterSideBar.SetActive(false);    
        }

        public void SetDisplay(RawImage display)
        {
            this.display = display;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Disaster == null)
            {
                return;
            }
            elementPanel.SetActive(false);
            disasterSideBar.SetActive(true);
            var disasterRawImage = disasterSideBar.GetComponentsInChildren<RawImage>()[0];
            disasterRawImage.texture = Disaster.Texture;
            var disasterConseguenceImage = disasterSideBar.GetComponentsInChildren<RawImage>()[1];
            disasterConseguenceImage.texture = Disaster.ConsiquencesTexture;

            disasterSideBar.GetComponentsInChildren<Text>()[0].text = Disaster.Name;
            disasterSideBar.GetComponentsInChildren<Text>()[1].text = Disaster.ShortDescription;
            disasterSideBar.GetComponentsInChildren<Text>()[3].text = Disaster.Reason;
            disasterSideBar.GetComponentsInChildren<Text>()[5].text = Disaster.Consiquences;

            Color rawImageColor = display.color;

            rawImageColor.a = 0.3f;

            display.color = rawImageColor;
        }
    }
}
