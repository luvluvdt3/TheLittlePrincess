using System;
using UnityEngine;
using UnityEngine.UI;
//this script is used in the lap selector from the play menu scene
public class LapSelector : MonoBehaviour
{
    [SerializeField] private GameObject LapUp, LapDown, Laps, LapsPanel;//here we declare all the buttons and labels that we are going to use
    public static int nLaps;//here we create a STATIC int to import it from the script LapsSelectedManager.cs
    [Header("Change the maximum of laps here:")]
    [SerializeField] private int MaximumLaps;
    private int ReactivatePlusButton;

    private void Start()
    {
        nLaps = Convert.ToInt32(Laps.GetComponent<Text>().text);
        ReactivatePlusButton = MaximumLaps - 1;
    }
    //with the + button in the game, the void Up is called and 1 more lap is added to the race
    public void Up()
    {
        nLaps = Convert.ToInt32(Laps.GetComponent<Text>().text) + 1;//the number of selected laps goes 1 up
        Laps.GetComponent<Text>().text = Convert.ToString(nLaps);//here we convert the int to string to show it in the UI text from the laps panel
        //the max of laps its 5 and it deactivates the + button
        //IMPORTANT: if you want more than 5 laps, change the maximum from the Unity inspector
        if (Laps.GetComponent<Text>().text == MaximumLaps.ToString())//maximum of laps
        {
            LapUp.SetActive(false);//it deactivates the lap + button because you can't add more laps once you reach the maximum
        }
        //reactivate - button
        if (Laps.GetComponent<Text>().text == "3")
        {
            LapDown.SetActive(true);//if you go from 2 (minimum) to 3, the - button reactivates
        }
    }
    //with the - button in the game, the void Down is called and 1 lap is removed from the race
    public void Down()
    {
        nLaps = Convert.ToInt32(Laps.GetComponent<Text>().text) - 1;//the number of selected laps goes 1 less
        Laps.GetComponent<Text>().text = Convert.ToString(nLaps);//here we convert the int to string to show it in the UI text from the laps panel
        //the min of laps its 2 and it deactivates the - button
        if (Laps.GetComponent<Text>().text == "2")
        {
            LapDown.SetActive(false);
        }
        //reactivate + button
        if (Laps.GetComponent<Text>().text == ReactivatePlusButton.ToString())
        {
            LapUp.SetActive(true);//if you go to the one number less than the maximum of laps, the + button reactivates
        }
    }
}
