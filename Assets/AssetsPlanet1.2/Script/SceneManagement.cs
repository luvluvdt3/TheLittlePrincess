using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    bool timerOn = false;
    public float timer = 2f;
    public GameObject player;
    public GameObject pet;
    public GameObject circle;


    void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                loadSceneOne();
            }
            if (timer <= 1f)
            {
                player.SetActive(false);
                pet.SetActive(false);
                circle.SetActive(false);
            }
        }
    }

    public void turnBack()
    {
        timerOn = true;
    }

    public void loadSceneOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        PlayerMovement.isBack = true;
    }
}
