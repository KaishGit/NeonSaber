using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    static public SfxManager Instance;

    public AudioSource _AudioSource;

    public AudioClip ShotAttack, ShotInHero, ShotInMonster, ShotInBoss, ShotInWall, ShotInShield, ShotInSaber;
    public AudioClip SaberPower, SaberInHero, SaberInMonster, SaberInBoss, SaberInWall, SaberInShield, SaberInSaber;
    public AudioClip DeathHero, DeathMonster, DeathBoss;
    public AudioClip ShieldActive, ButtonClick;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayShotAttack()
    {
        _AudioSource.PlayOneShot(ShotAttack);
    }

	public void PlayShotInHero()
	{
		_AudioSource.PlayOneShot(ShotInHero);
	}

	public void PlayShotInMonster()
	{
		_AudioSource.PlayOneShot(ShotInMonster);
	}

	public void PlayShotInBoss()
	{
		_AudioSource.PlayOneShot(ShotInBoss);
	}

	public void PlayShotInWall()
	{
		_AudioSource.PlayOneShot(ShotInWall);
	}

	public void PlayShotInShield()
	{
		_AudioSource.PlayOneShot(ShotInShield);
	}

	public void PlayShotInSaber()
	{
		_AudioSource.PlayOneShot(ShotInSaber);
	}

	public void PlaySaberPower()
	{
		_AudioSource.PlayOneShot(SaberPower);
	}

	public void PlaySaberInHero()
	{
		_AudioSource.PlayOneShot(SaberInHero);
	}

	public void PlaySaberInMonster()
	{
		_AudioSource.PlayOneShot(SaberInMonster);
	}

	public void PlaySaberInBoss()
	{
		_AudioSource.PlayOneShot(SaberInBoss);
	}

	public void PlaySaberInWall()
	{
		_AudioSource.PlayOneShot(SaberInWall);
	}

	public void PlaySaberInShield()
	{
		_AudioSource.PlayOneShot(SaberInShield);
	}

	public void PlaySaberInSaber()
	{
		_AudioSource.PlayOneShot(SaberInSaber);
	}

	public void PlayDeathHero()
	{
		_AudioSource.PlayOneShot(DeathHero);
	}

	public void PlayDeathMonster()
	{
		_AudioSource.PlayOneShot(DeathMonster);
	}

	public void PlayDeathBoss()
	{
		_AudioSource.PlayOneShot(DeathBoss);
	}

	public void PlayShieldActive()
	{
		_AudioSource.PlayOneShot(ShieldActive);
	}

	public void PlayButtonClick()
	{
		_AudioSource.PlayOneShot(ButtonClick);
	}

}