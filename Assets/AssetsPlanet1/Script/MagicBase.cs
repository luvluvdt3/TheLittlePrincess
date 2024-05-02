using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicBase : MonoBehaviour
{
    public GameObject player;
    public GameObject magicEffect;
    //set timer start from when player enter the magic base
    public float timer = 7f;
    bool timerOn = false;
    PlayerMovement playerMovement;
    public GameObject transitEffect;
    public GameObject pet;



    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player.transform)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.playMagic();
            timerOn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player.transform)
        {
            playerMovement.stopRotating();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MagicBase>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = false;
            transitEffect.SetActive(false);
            player.SetActive(true);
            pet.SetActive(true);
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
                //load next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
            if (timer <= 6f)
            {
                player.transform.position = transform.position;
                playerMovement.stopMagic();
                playerMovement.startRotating();
                magicEffect.SetActive(true);
            }
            if (timer <= 2.5f)
            {
                gameObject.GetComponent<AudioSource>().enabled = true;
                transitEffect.SetActive(true);
            }
            if(timer <= 1.5f)
            {
                player.SetActive(false);
                pet.SetActive(false);
            }




        }
        
    }
}
