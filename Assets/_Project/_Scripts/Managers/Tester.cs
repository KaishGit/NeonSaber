using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LevelManager.Instance.NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            LevelManager.Instance.PrevLevel();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LevelManager.Instance.RestartLevel();
        }
    }
}
