using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject player;
    SplineFollower splineFollower;
    Animator anim;
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 40)
        {

            splineFollower.followSpeed = 20f;
            anim.enabled = true;
        }
        else
        {
            splineFollower.followSpeed = 0f;
            anim.enabled = false;

        }

    }

}
