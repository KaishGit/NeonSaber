using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberEffectManager : MonoBehaviour
{
    static public SaberEffectManager Instance;

    public float DelayTime, DelayTimeRange;
    public int SkillAmount;
    public List<SaberControl> SaberList;
    public bool dontChange;

    private SaberControl currentSaber, tempSaber;
    private float nextDrawnTime;
    private bool neutralSaber = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentSaber = SaberList[0];
        nextDrawnTime = Time.time + DelayTime + Random.Range(-DelayTimeRange, DelayTimeRange);
    }

    private void Update()
    {
        if (dontChange && currentSaber == SaberList[0]) return;

        if (Time.time >= nextDrawnTime)
        {
            neutralSaber = !neutralSaber;

            if (neutralSaber)
            {
                tempSaber = SaberList[0];

                nextDrawnTime = Time.time + DelayTime + Random.Range(-DelayTimeRange, DelayTimeRange);
            }
            else
            {
                tempSaber = SaberList[Random.Range(1, SkillAmount + 1)];

                nextDrawnTime = Time.time + tempSaber.duration;
            }

            tempSaber.gameObject.SetActive(true);

            tempSaber._RigidBody.velocity = currentSaber._RigidBody.velocity;
            tempSaber.gameObject.transform.position = currentSaber.gameObject.transform.position;
            tempSaber.gameObject.transform.rotation = currentSaber.gameObject.transform.rotation;

            currentSaber.gameObject.SetActive(false);
            currentSaber = tempSaber;

            VfxManager.Instance.PlaySaberPower(currentSaber.transform.position);
            SfxManager.Instance.PlaySaberPower();
        }
    }

    public void SetSaberByBoss(SaberControl saber)
    {
        Debug.Log("Bateu");

        neutralSaber = false;

        if(saber != currentSaber)
        {
            tempSaber = saber;

            tempSaber.gameObject.SetActive(true);
            Debug.Log(tempSaber.gameObject.name);

            tempSaber._RigidBody.velocity = currentSaber._RigidBody.velocity;
            tempSaber.gameObject.transform.position = currentSaber.gameObject.transform.position;
            tempSaber.gameObject.transform.rotation = currentSaber.gameObject.transform.rotation;

            currentSaber.gameObject.SetActive(false);
            currentSaber = tempSaber;
        }

        nextDrawnTime = Time.time + tempSaber.duration;
        VfxManager.Instance.PlaySaberPower(currentSaber.transform.position);
        SfxManager.Instance.PlaySaberPower();
    }
}
