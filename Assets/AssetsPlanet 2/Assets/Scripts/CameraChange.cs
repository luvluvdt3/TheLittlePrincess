using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    //change the active camera during the race with V key, in input manager you can change the key in Viewmode input
    [SerializeField] private Camera NormalCam, FarCam, FPCam;//set the different cameras from inspector or add more in here
    [Header("Edit > Project Settings > Input - Set your axes name here:")]
    [SerializeField] private string ButtonName;
    private int CamMode;

    void Update()
    {
        if (Input.GetButtonDown(ButtonName))//if you press the camera change keyboard button
        {
            if (CamMode == 2)//once you reach the last camera
            {
                CamMode = 0;//you go back to the first one
            }
            else
            {
                CamMode += 1;//if you are not in the last camera, the next one is showed
            }
            StartCoroutine(ChangeCamera());//the camera changes because of the ChangeCamera coroutine
        }
    }

    IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(0.01f);//it waits for the next frame
        //and sets the corresponding camera for each number and deactivates the other ones
        if (CamMode == 0)
        {
            NormalCam.gameObject.SetActive(true);
            FPCam.gameObject.SetActive(false);
        }
        if (CamMode == 1)
        {
            FarCam.gameObject.SetActive(true);
            NormalCam.gameObject.SetActive(false);
        }
        if (CamMode == 2)
        {
            FPCam.gameObject.SetActive(true);
            FarCam.gameObject.SetActive(false);
        }
    }
}