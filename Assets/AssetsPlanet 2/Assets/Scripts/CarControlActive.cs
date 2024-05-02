using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarControlActive : MonoBehaviour
{
    //Activate car controls after the race countdown (3, 2, 1, go)
    //This script it's activated from the "Countdown" script
    private FreezePos[] FreezeScripts;
    private UnfreezePos[] UnfreezeScripts;
    private CarAIControl[] CarAIControlScripts;
    private AICarBehaviour[] AICarBehaviourScripts;

    void Start()
    {
        FreezeScripts = FindObjectsOfType(typeof(FreezePos)) as FreezePos[];
        UnfreezeScripts = FindObjectsOfType(typeof(UnfreezePos)) as UnfreezePos[];
        CarAIControlScripts = FindObjectsOfType(typeof(CarAIControl)) as CarAIControl[];
        AICarBehaviourScripts = FindObjectsOfType(typeof(AICarBehaviour)) as AICarBehaviour[];
        foreach (FreezePos fp in FreezeScripts)
            fp.enabled = false;
        foreach (UnfreezePos up in UnfreezeScripts)
            up.enabled = true;
        foreach (CarAIControl cc in CarAIControlScripts)
            cc.enabled = true;
        foreach (AICarBehaviour ac in AICarBehaviourScripts)
            ac.enabled = true;
    }
}