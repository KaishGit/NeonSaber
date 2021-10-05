using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    static public VfxManager Instance;

	public GameObject HeroDeath, MonsterDeath, BossDeath;
	public GameObject SaberPower, DamageBoss, DarkHero, Bullet;

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

	public void PlayHeroDeath(Vector3 position)
	{
		Instantiate(HeroDeath, position, Quaternion.identity, gameObject.transform);
	}

	public void PlayMonsterDeath(Vector3 position)
	{
		Instantiate(MonsterDeath, position, Quaternion.identity, gameObject.transform);
	}

	public void PlayBossDeath(Vector3 position)
	{
		Instantiate(BossDeath, position, Quaternion.identity, gameObject.transform);
	}

	public void PlaySaberPower(Vector3 position)
	{
		Instantiate(SaberPower, position, Quaternion.identity, gameObject.transform);
	}

	public void PlayDamageBoss(Vector3 position)
	{
		Instantiate(DamageBoss, position, Quaternion.identity, gameObject.transform);
	}

	public void PlayDarkHero(Vector3 position)
	{
		Instantiate(DarkHero, position, Quaternion.identity, gameObject.transform);
	}

	public void PlayBullet(Vector3 position)
	{
		Instantiate(Bullet, position, Quaternion.identity, gameObject.transform);
	}
}

