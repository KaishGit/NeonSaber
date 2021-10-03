using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            VfxManager.Instance.PlayBossDeath(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TrackManager.Instance.PlayStage02();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TrackManager.Instance.PlayCredits();
        }       
    }
}
