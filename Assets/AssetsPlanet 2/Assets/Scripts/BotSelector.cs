using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script is used in the bot selector from the play menu
public class BotSelector : MonoBehaviour
{
    private List<GameObject> AICarsList = new List<GameObject>();
    private GameObject[] AICars;
    [SerializeField] private GameObject BotUp, BotDown, Bots, BotsPanel;//here we declare all the buttons and labels that we are going to use
    public static int nBots;//here we create a STATIC int to import it from the script BotsSelectedManager.cs    
    private int ReactivatePlusButton;

    private void Start()
    {
        nBots = 1;//at the start of the game, the number of selected bots is 1

        AICars = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in AICars)
        {
            if (go.name.Contains("AICar"))
            {
                AICarsList.Add(go);
            }
        }

        ReactivatePlusButton = (AICarsList.Count / 2);
        ReactivatePlusButton--;
    }
    //with the + button in the game, the void Up is called and 1 more bot is added to the race
    public void Up()
    {
        nBots = Convert.ToInt32(Bots.GetComponent<Text>().text) + 1;//the number of selected bots goes 1 up
        Bots.GetComponent<Text>().text = Convert.ToString(nBots);//here we convert the int to string to show it in the UI text from the bots panel
        //the max of bots it's 3 and it deactivates the + button
        //IMPORTANT: change the maximum of bots from the Unity inspector
        if (Bots.GetComponent<Text>().text == (AICarsList.Count/2).ToString())//maximum of bots
        {
            BotUp.SetActive(false);//it deactivates the bot + button because you can't add more bots
        }
        //reactivate - button
        if (Bots.GetComponent<Text>().text == "2")
        {
            BotDown.SetActive(true);//button - gets activated again because the number of selected bots it's not 1 (the minimum)
        }
    }
    //with the - button in the game, the void Down is called and 1 bot is removed from the race
    public void Down()
    {
        nBots = Convert.ToInt32(Bots.GetComponent<Text>().text) - 1;//the number of selected bots goes 1 less
        Bots.GetComponent<Text>().text = Convert.ToString(nBots);//here we convert the int to string to show it in the UI text from the bots panel
        //the min of bots it's 1 and it deactivates the - button
        if (Bots.GetComponent<Text>().text == "1")
        {
            BotDown.SetActive(false);
        }
        //reactivate + button
        if (Bots.GetComponent<Text>().text == ReactivatePlusButton.ToString())
        {
            BotUp.SetActive(true);//if you go to the one number less than the maximum of bots, the + button reactivates
        }
    }
}
