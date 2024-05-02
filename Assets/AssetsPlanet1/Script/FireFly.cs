using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    public GameObject player;
    SplineFollower splineFollower;
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 40)
        {
 
            splineFollower.followSpeed = 20f;
        }
        else
        {
            splineFollower.followSpeed = 0f;

        }
        
    }
}
