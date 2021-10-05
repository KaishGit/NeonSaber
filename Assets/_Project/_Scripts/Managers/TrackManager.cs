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

            _AudioSource.clip = Menu;
            _AudioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }       
    }

    private void Update()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        // Menu / Final
        if (sceneIndex == 0 || sceneIndex == 17)
        {
            PlayMenu();
        }
        // cutscenes
        else if (sceneIndex == 1 || sceneIndex == 5 || sceneIndex == 8 || sceneIndex == 11 || sceneIndex == 14 || sceneIndex == 16) 
        {
            PlayCredits();
        }
        // Levels
        else
        {
            PlayGamePlay();
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
        if (_AudioSource.clip == Stage01) return;

        PlayStage(Stage01);
    }

    public void PlayMenu()
    {
        if (_AudioSource.clip == Menu) return;

        _AudioSource.Stop();
        _AudioSource.time = 0;
        _AudioSource.clip = Menu;
        _AudioSource.Play();
        isStageTrack = false;
    }

    public void PlayCredits()
    {
        if (_AudioSource.clip == Credits) return;

        _AudioSource.Stop();
        _AudioSource.time = 0;
        _AudioSource.clip = Credits;      
        _AudioSource.Play();
        isStageTrack = false;
    }
}
