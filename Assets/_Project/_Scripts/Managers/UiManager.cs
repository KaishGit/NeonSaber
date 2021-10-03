using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	static public UiManager Instance;

	private bool clockStarted;

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

	private void Update()
	{
		if (clockStarted)
		{
			//updateClock
		}
	}

	public void StartClock()
	{
		clockStarted = true;
		Debug.Log("TODO: Clock Started...");
	}

	private void ShowBossName()
	{
		Debug.Log("TODO: Show Boss name...");
	}
}
