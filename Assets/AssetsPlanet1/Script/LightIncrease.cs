using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIncrease : MonoBehaviour
{
    private Light lightning;
    void Start()
    {
        lightning = GetComponent<Light>();
    }

    public void IncreaseLight()
    {
        lightning.intensity += 0.05f;
    }
}
