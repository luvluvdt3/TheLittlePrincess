using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject player;
    public GameObject pet;
    public GameObject transitEffect;
    public GameObject magicEffect;
    public float timer = 3f;
    bool timerOn = false;
    PlayerMovement playerMovement;
 

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player.transform)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.stopMovement();
            timerOn = true;
        }
    }
    void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;
            print(timer);
            if (timer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
                return;
            }
            if (timer <= 2.5f)
            {
                gameObject.GetComponent<AudioSource>().enabled = true;
                transitEffect.SetActive(true);
            }
            if (timer <= 1.5f)
            {
                player.SetActive(false);
                pet.SetActive(false);
                magicEffect.SetActive(false);
            }
        }
    }
}
