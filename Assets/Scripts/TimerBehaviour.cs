using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerBehaviour : MonoBehaviour
{

    public float time;
    public Slider TimeBar;


    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        //timerText.text = "" + Mathf.Round(startingTime);


    }

    private void FixedUpdate()
    {
        TimeBar.value = time;
    }

}
