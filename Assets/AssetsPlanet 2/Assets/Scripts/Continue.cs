using UnityEngine;
using UnityEngine.SceneManagement;
//this script is used in the Play button from the first menu and the Continue button when you finish the race
public class Continue : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, LapsBotsPanel, Countdown, FinishCamera, Checkpoints, LapsSelected, BotsSelected;
    //also, it is used if we hit restart in the pause menu
    public void Restart()
    {   //void restart is used in continue buttons when we finish the race and in return button of the pause menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1); //we load the scene again
        PlayerMovement.isBack = true;
    }
    //if we hit play in the menu at the start of the scene we will use the Play void:
    public void Play()
    {   //all the racing stuff turns on
        RaceUI.SetActive(true); //racing UI
        Countdown.SetActive(true);  //countdown UI (3,2,1,go)
        Checkpoints.SetActive(true); //racetrack checkpoints
        LapsSelected.SetActive(true); //turn on the lap requirement race-UI text
        BotsSelected.SetActive(true); //turns off the bots depending the amount selected by the player
        LapsBotsPanel.SetActive(false); //we turn off the start panel with the laps and bots
        FinishCamera.SetActive(false); //and the camera goes off too, to use the one in the player car
    }
}
