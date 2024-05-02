using UnityEngine;
using UnityEngine.UI;
//this script is used when the player completes all the laps of the race (when the race finishes)
public class RaceFinish : MonoBehaviour
{
    //finish camera gets activated
    [SerializeField] private GameObject FinishCam;
    //viewmodes gets deactivated so you can't change the camera once the race is over
    [SerializeField] private GameObject ViewModes;
    //Race UI gets deactivated once the race finishes
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2;
    //the different finish panels (if you win the race or lose)
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose;

    void OnTriggerEnter()//the race finish trigger will activate when ChkManager.cs script detects that you completed all laps
    {
        this.GetComponent<BoxCollider>().enabled = false;//the race finish trigger collider turns off to avoid triggering twice
        FinishCam.SetActive(true);//finish camera gets activated
        //Race UI gets deactivated once the race finishes
        PauseButton.SetActive(false);     Panel1.SetActive(false);        Panel2.SetActive(false);
        //viewmodes gets deactivated so you can't change the camera once the race is over
        ViewModes.SetActive(false);

        //if you win (you finish 1st position)
        if (PosDisplay.GetComponent<Text>().text == "1st Place")
        {
            FinishPanelWin.SetActive(true);//win panel turns on
            FinishPanelLose.SetActive(false);//lose panel turns off
        }
        //you lose (not 1st position)
        else
        {
            FinishPanelWin.SetActive(false);//win panel turns off
            FinishPanelLose.SetActive(true);//lose panel turns on
            AudioListener.volume = 0f;//audio turns off
            Time.timeScale = 0;//time stops
        }
    }
}