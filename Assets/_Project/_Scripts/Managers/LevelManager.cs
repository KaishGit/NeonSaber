using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static public LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        var newSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(newSceneIndex == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(newSceneIndex);
    }

    public void PrevLevel()
    {
        var newSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if (newSceneIndex == -1)
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        else
            SceneManager.LoadScene(newSceneIndex);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
