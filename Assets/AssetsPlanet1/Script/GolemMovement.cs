using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        if (PlayerMovement.isBack)
        {
            gameObject.SetActive(false);
        }
        else
        {
            animator = GetComponent<Animator>();
            animator.speed = 0f;
        }
    }
}
