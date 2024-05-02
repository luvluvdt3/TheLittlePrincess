using UnityEngine;

public class BotsSelectedManager : MonoBehaviour
{
    private GameObject[] AICars;
    private string AICarString;
    private int AICarNumber;

    void Awake()
    {
        AICars = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in AICars)
        {
            if (go.name.Contains("AICar"))
            {
                if (go.name.Length == 6)//one-digit number AI car name has 6 characters (for example: AICar4)
                {
                    AICarString = go.name.Substring(go.name.Length - 1);//get only the number of the name (the last character)
                    AICarNumber = int.Parse(AICarString);
                }
                else if (go.name.Length == 7)//two-digit number AI car name has 7 characters (for example: AICar15)
                {
                    AICarString = go.name.Substring(go.name.Length - 2);//get the number of the name (the last 2 characters)
                    AICarNumber = int.Parse(AICarString);
                }
                
                if (go.name == "AICar" + AICarNumber)
                {
                    if (AICarNumber > BotSelector.nBots)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
    }
}
