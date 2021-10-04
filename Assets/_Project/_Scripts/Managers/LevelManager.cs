using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static public LevelManager Instance;
    public float RestartDelay, LoadDelay;

    private WaitForSeconds restartWait, loadWait;

    private void Awake()
    {
        Instance = this;

        //if (Instance == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        restartWait = new WaitForSeconds(RestartDelay);
        loadWait = new WaitForSeconds(LoadDelay);
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
        StartCoroutine(WaitForLoad(index));
    }

    IEnumerator WaitForLoad(int index)
    {
        yield return loadWait;
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        StartCoroutine(WaitForRestart());
    }

    IEnumerator WaitForRestart()
    {
        yield return restartWait;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
