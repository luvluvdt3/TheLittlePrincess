using System;
using System.Collections.Generic;
using UnityEngine;
//this script is attached to all the checkpoints in the race and measures the car position to the checkpoint
public class Checkpoint : MonoBehaviour
{
    private float nDistanceX, nDistanceY, nDistanceZ;
    private List<double> nDistChk = new List<double>();
    private int nCheckpointNumber, kPos;
    //IMPORTANT NOTE: when adding new checkpoints follow the name scheme: Chk1, Chk2, ... Chk13, Chk14
    private void Start()
    {
        //from here we read the number of the checkpoint game object where this script is located
        for (int i = 0; i < this.name.Length; i++)
        {
            if (this.name.Substring(i, 1) == "k")//all checkpoints have names like: Chk1, Chk2, Chk3
            {
                kPos = i + 1;//so we take the number after the k character to know which checkpoint is this one
                break;
            }
        }

        if (this.name.Length != 3){ nCheckpointNumber = Convert.ToInt32(this.name.Substring(kPos, this.name.Length - kPos)); }
        
        nDistChk.Clear();//clear all distances used in previous races

        for (int i = 0; i <= BotSelector.nBots; i++)
        {
            nDistChk.Add(0);//add a distance meter check for each bot that you add to the race
        }       
    }

    void Update()
    {
        for (int i = 0; i <= BotSelector.nBots; i++)
        {
            if (ChkManager.nChk[i] == nCheckpointNumber)
            {
                //from here we can get the distance of the car position to the checkpoint position
                nDistanceZ = this.GetComponent<Transform>().localPosition.z - ChkManager.CarPosList[i].GetComponent<Transform>().position.z;
                nDistanceY = this.GetComponent<Transform>().localPosition.y - ChkManager.CarPosList[i].GetComponent<Transform>().position.y;
                nDistanceX = this.GetComponent<Transform>().localPosition.x - ChkManager.CarPosList[i].GetComponent<Transform>().position.x;
                nDistChk[i] = Math.Sqrt(Math.Pow(nDistanceX, 2) + Math.Pow(nDistanceY, 2) + Math.Pow(nDistanceZ, 2));
                ChkManager.nDistP[i] = nDistChk[i];//and we send the information to the ChkManager.cs script (checkpoint manager)
                //checkpoint manager will compare the distance, checkpoints passed and laps done of each car of the race to obtain real time positioning
            }
        }
    }
}
