using System;
using UnityEngine;
using UnityEngine.UI;
//from this script we will show in the racing UI the lap requirement with the number of laps selected
public class LapsSelectedManager : MonoBehaviour
{
    [SerializeField] private GameObject LapRequirement;//declare the lap requirement label of the Canvas
    
    private void Start()
    {
        LapRequirement.SetActive(true);//turn on the label
        LapRequirement.GetComponent<Text>().text = Convert.ToString(LapSelector.nLaps);//and apply the amount of laps selected as text
    }
}
