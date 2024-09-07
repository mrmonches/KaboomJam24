using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{

    public Slider TimeBar;


    public void SetMaxBar(int bar)
    {
        TimeBar.maxValue = bar;
        TimeBar.value = bar;
    }
   

    public void SetBar(int bar)
    {
        TimeBar.value = bar;
    }


}
