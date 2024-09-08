using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip MainLoop;

    [SerializeField] private float IntroTimer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine("LoopDelay");
    }

    private IEnumerator LoopDelay()
    {
        if (true)
        {
            yield return new WaitForSeconds(IntroTimer);

            _audioSource.PlayOneShot(MainLoop);

            _audioSource.loop = true;
        }
    }
}
