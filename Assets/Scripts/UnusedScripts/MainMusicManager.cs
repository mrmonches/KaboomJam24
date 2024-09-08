using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource MainLoop;
    [SerializeField] private AudioSource MainIntro;

    [SerializeField] private float IntroTimer;

    private void Awake()
    {
        StartCoroutine("LoopDelay");
    }

    private IEnumerator LoopDelay()
    {
        if (true)
        {
            yield return new WaitForSeconds(IntroTimer);

            MainIntro.Stop();

            MainLoop.Play();
        }
    }
}
