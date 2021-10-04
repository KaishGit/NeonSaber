using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    private bool isFinish;

    private void Awake()
    {
        Instance = this;
    }

    private List<GameObject> enemyList;

    void Start()
    {
        enemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Boss"));
    }

    void Update()
    {
        if(!isFinish && enemyList.Count <= 0)
        {
            GameWin();
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void SetGameOver()
    {
        if (isFinish) return;

        GameLose();
    }

    private void GameWin()
    {
        isFinish = true;
        Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>());
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("Hero_Idle");
        LevelManager.Instance.NextLevel();
        Debug.Log("GameWin");
    }

    private void GameLose()
    {
        LevelManager.Instance.RestartLevel();
        isFinish = true;
        Debug.Log("GameLose");
    }
}
