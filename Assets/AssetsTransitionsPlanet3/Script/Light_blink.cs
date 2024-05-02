using System.Collections;
using UnityEngine;

public class Light_blink : MonoBehaviour
{
    public Light lightToBlink;
    public float blinkInterval = 1f;

    void Start()
    {
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            lightToBlink.enabled = !lightToBlink.enabled;
            if(blinkInterval != 0f)
            {
                blinkInterval -= 0.5f;
            }
            else
            {
                blinkInterval = 2f;
            }
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
