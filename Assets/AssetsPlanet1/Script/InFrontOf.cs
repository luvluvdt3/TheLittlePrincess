using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOf : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;

    private void LateUpdate()
    {
        transform.position = target.position + target.forward * distance;
        transform.LookAt(target);
    }
}
