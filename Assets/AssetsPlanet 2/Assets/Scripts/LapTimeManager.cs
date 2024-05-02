using UnityEngine;
using UnityEngine.UI;
//this script makes the race time work
public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount, SecondCount;//minutes and seconds are ints
    public static float MilliCount;//milliseconds are floats
    public static string MilliDisplay;//milliseconds are also stated as strings to show less numbers after the comma (,)
    [SerializeField] private GameObject MinuteBox, SecondBox, MilliBox;//and we need 3 game objects in the canvas to show the values in race UI

    //this void will make the clock timer of the race work
    void Update()
    {
        MilliCount += Time.deltaTime * 10;
        MilliDisplay = MilliCount.ToString("F0");//here we set the milliseconds display numbers after the comma
        MilliBox.GetComponent<Text>().text = "" + MilliDisplay;

        if (MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;
        }

        if (SecondCount <= 9)
        {
            SecondBox.GetComponent<Text>().text = "0" + SecondCount + ".";
        }
        else
        {
            SecondBox.GetComponent<Text>().text = "" + SecondCount + ".";
        }

        if (SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        if (MinuteCount <= 9)
        {
            MinuteBox.GetComponent<Text>().text = "0" + MinuteCount + ":";
        }
        else
        {
            MinuteBox.GetComponent<Text>().text = "" + MinuteCount + ":";
        }
    }
}