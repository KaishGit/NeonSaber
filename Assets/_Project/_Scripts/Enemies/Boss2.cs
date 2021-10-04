using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy
{
    public SaberControl Saber;

    protected override void OnUpdate()
    {
        switch (life)
        {
            case 4:
                Phase1();
                break;
            case 3:
                Phase2();
                break;
            case 2:
            case 1:
                Phase3();
                break;
        }

        //Movement();
    }

    private void Phase3()
    {
        //throw new NotImplementedException();
    }

    private void Phase2()
    {
        //throw new NotImplementedException();
    }

    private void Phase1()
    {
        //throw new NotImplementedException();
    }

    protected override void OnTakeDamage(string tag)
    {

        if (tag == "Sabre")
        {
            SfxManager.Instance.PlaySaberInBoss();
            SaberEffectManager.Instance.SetSaberByBoss(Saber);
        }
        else
        {
            SfxManager.Instance.PlayShotInBoss();
        }

        base.OnTakeDamage(tag);
    }

    protected override void OnDeath()
    {
        VfxManager.Instance.PlayBossDeath(transform.position);
        StartCoroutine(DeathCoroutine());

        base.OnDeath();
    }

    IEnumerator DeathCoroutine()
    {
        sprRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        SfxManager.Instance.PlayDeathBoss();
        Destroy(gameObject);
    }
}
