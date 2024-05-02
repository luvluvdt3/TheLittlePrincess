using UnityEngine;

public class TimeScript : MonoBehaviour
{
    //this script is used to pause/resume time when the race ends or in the pause menu
    //also pressing tab you can go 3x times faster (tip: pause and despause with 'esc' key to go back to normal speed)
    [SerializeField] private GameObject PauseMenu;
    public bool Paused;

    public void TimeScale0()
    {
        Time.timeScale = 0;//stop the time
    }
    public void TimeScale1()
    {
        AudioListener.volume = 1f;//reactivate audio
        Time.timeScale = 1;//resume time
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeState();
        }
        //fast time mode with 'tab' key
        if (Input.GetKeyDown("tab") && Time.timeScale == 1)
        {
            Time.timeScale = 3;
        }
    }
    //when you press 'esc' key, pause void activates
    public void Pause()
    {
        ChangeState();//and changes the pause state
    }
    //changing state:
    private void ChangeState()
    {
        Paused = !Paused;
        if (Paused)
        {
            //pausing will turn off audio and stop time
            AudioListener.volume = 0f;
            Time.timeScale = 0;
            PauseMenu.SetActive(true); //show the pause menu (to resume or restart race)
        }
        else
        {
            //unpausing reactivates audio and resumes normal time
            AudioListener.volume = 1f;
            Time.timeScale = 1;
            PauseMenu.SetActive(false); //turn off the pause menu
        }
    }
}

