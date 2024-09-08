using System.Collections;
using UnityEngine;

public class VictoryMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource VictoryIntro;
    [SerializeField] private AudioSource VictoryLoop;

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

            VictoryLoop.Play();

            VictoryIntro.Stop();
        }
    }
}
