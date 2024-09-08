using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimerBehaviour : MonoBehaviour
{

    public float time;
    public Slider TimeBar;


    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gm = GameObject.FindObjectOfType<GameManager>();

        time -= Time.deltaTime;
        //timerText.text = "" + Mathf.Round(startingTime);

        if (time <= 0)
        {
            if (gm.money >= gm.maxMoney)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                SceneManager.LoadScene("LoseScene");
            }

        }

    }

    private void FixedUpdate()
    {
        TimeBar.value = time;
    }



}
