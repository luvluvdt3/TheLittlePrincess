using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class CloseClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject weatherSideDisplay;
    public GameObject elementsToDisplay;
    private RawImage rawImage;
    private float originalOpacity;
    public float addHoverOpacity = 0.5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (weatherSideDisplay != null)
        {

            weatherSideDisplay.SetActive(false);
            if(weatherSideDisplay.name == "DisasterPanel")
            {
                weatherSideDisplay.transform.position = new Vector3(weatherSideDisplay.transform.position.x, -1200f, weatherSideDisplay.transform.position.z);
                DialogueLua.SetVariable("Points", DialogueLua.GetVariable("Points").asInt + 1);
                print(DialogueLua.GetVariable("Points").asInt);
                DialogueManager.SendUpdateTracker();

                if (DialogueLua.GetVariable("Points").asInt >= 10)
                {
                    DialogueManager.ShowAlert("Congratulations! You have finished the quest! ", 5);
                    PlayerMovement3.isBack = true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }
            if(elementsToDisplay != null)
            {
                elementsToDisplay.SetActive(true);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeOpacity(originalOpacity + addHoverOpacity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeOpacity(originalOpacity);
    }

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        if (rawImage != null)
            originalOpacity = rawImage.color.a;
        
    }

    private void ChangeOpacity(float opacity)
    {
        if (rawImage == null)
            return;
        
        Color newColor = rawImage.color;
        newColor.a = opacity;
        rawImage.color = newColor;
    }
}
