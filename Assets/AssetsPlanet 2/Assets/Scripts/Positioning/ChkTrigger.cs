using System;
using UnityEngine;

public class ChkTrigger : MonoBehaviour
{
    //this script takes the count of the checkpoints and laps passed of each player in the race
    //and then calculates it as score with the distance from the last passed checkpoint
    //at the end, the information is processed in ChkManager script (distance, chks and laps passed)
    public static bool startDis;
    private int nCheckpointNumber, kPos, CurrentChk, NextChk;
    public int CarPosListNumber;

    private void Start()
    {
        if (transform.parent.name.Length == 6)//one-digit number name has 6 characters (for example: AICar4)
        {
            CarPosListNumber =  int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1));//get the last character (4)
        }
        if (transform.parent.name.Length == 7)//two-digit numbers name has 7 characters (for example: AICar17)
        {
            CarPosListNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 2));//get the last 2 characters (17)
        }
        if (CarPosListNumber == 0)
        {
            gameObject.name = "CarPos" + CarPosListNumber;
        }
        else
        {
            if (transform.parent.name.Length == 6)//one-digit number name has 9 characters (for example: CarPosAI4)
            {
                gameObject.name = "CarPosAI0" + CarPosListNumber;
            }
            if (transform.parent.name.Length == 7)//one-digit number name has 9 characters (for example: CarPosAI4)
            {
                gameObject.name = "CarPosAI" + CarPosListNumber;
            }
        }
        CurrentChk = 0;
        NextChk = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Substring takes the last number of the Chk
        if (other.gameObject.name.Substring(0, 3) == "Chk")
        {
            ChkManager.nDistP[CarPosListNumber]/*position 0 in chk manager arrays stands for player 1*/  = 0;//set the distance from checkpoint to 0 when crossing it
            for (int i = 0; i < this.name.Length; i++)
            {
                if (other.gameObject.name.Substring(i, 1) == "k")
                {
                    kPos = i + 1;
                    break;
                }
            }
            //get the last character of the string which is the checkpoint number (i.e: "chk2", the last character is 2)
            nCheckpointNumber = Convert.ToInt32(other.gameObject.name.Substring(kPos, other.gameObject.name.Length - kPos));
            if (CurrentChk + 1 == nCheckpointNumber)
            {
                ChkManager.nChk[CarPosListNumber]/*position 0 in chk manager arrays stands for player 1*/ = nCheckpointNumber;
                CurrentChk += 1;
                NextChk += 1;

                if (nCheckpointNumber == 1)//if the next checkpoint you have to pass is number 1
                {//that means that you passed all the checkpoints
                    startDis = true;
                    ChkManager.nLapsP[CarPosListNumber]/*position 0 in chk manager arrays stands for player 1*/ += 1;//and a lap is added to the lap counter
                    CurrentChk = 1;//and the current checkpoint will be 1 (being 2 the next checkpoint)
                    if (CarPosListNumber == 0)//if trigchk it's located in player 1
                    {
                        //the lap time will reset to 0 to make a new one
                        LapTimeManager.MinuteCount = 0;
                        LapTimeManager.SecondCount = 0;
                        LapTimeManager.MilliCount = 0;
                    }
                }
            }
            //if the next checkpoint doesn't exist, then you reached the last checkpoint of the circuit
            if (GameObject.Find("Chk" + NextChk) == null)
            {
                //so the checkpoint counter resets and gets ready to receive the first checkpoint again
                CurrentChk = 0;
                NextChk = 1;
            }
        }
    }
}