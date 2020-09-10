using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isPlaying;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            isPlaying = audioSource.isPlaying;
        }
    }

    public void PlayAudio()
    {
        audioSource.Play();
        isPlaying = true;
        isPaused = false;
    }

    public void PauseAudio()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            isPaused = true;
            isPlaying = false;
        }
    }

    public void ResumeAudio()
    {
        audioSource.UnPause();
        isPaused = false;
        isPlaying = true;
    }

    public void EndAudio()
    {
        audioSource.Stop();
        isPlaying = false;
        isPaused = false;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
