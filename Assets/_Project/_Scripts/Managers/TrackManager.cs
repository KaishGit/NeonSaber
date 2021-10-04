using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    static public TrackManager Instance;

    public AudioSource _AudioSource;
    public AudioClip Stage01, Menu, Credits;

    private float currentTime;
    private bool isStageTrack;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _AudioSource.clip = Stage01;
            _AudioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }       
    }

    private void PlayStage(AudioClip newAudioClip)
    {
        if (isStageTrack)
        {
            currentTime = _AudioSource.time;
            _AudioSource.clip = newAudioClip;
            _AudioSource.time = currentTime;
        }
        else
        {
            _AudioSource.Stop();
            _AudioSource.time = 0;
            _AudioSource.clip = newAudioClip;
        }

        isStageTrack = true;
        _AudioSource.Play();
    }

    public void PlayGamePlay()
    {
        PlayStage(Stage01);
    }

    public void PlayMenu()
    {
        _AudioSource.Stop();
        _AudioSource.time = 0;
        _AudioSource.clip = Menu;
        _AudioSource.Play();
        isStageTrack = false;
    }

    public void PlayCredits()
    {
        _AudioSource.Stop();
        _AudioSource.time = 0;
        _AudioSource.clip = Credits;      
        _AudioSource.Play();
        isStageTrack = false;
    }
}
