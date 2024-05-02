using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public GameObject gameEndingImg;
    private bool isEnding = false;
    public float timer = 4f;


    public void EndGame()
    {
        print (DialogueLua.GetVariable("Points").asInt);
        if(DialogueLua.GetVariable("Points").asInt >= 10)
        {
            isEnding = true;
            print("True");
        }
    }

    void Update()
    {
        if (isEnding)
        {
            print("Start Counting");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(0);
                print("End Game");
            }
            if (timer <= 4f)
            {
                gameEndingImg.SetActive(true);
            }
        }
    }
}
