using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5 : Enemy
{
    protected override void OnUpdate()
    {
        switch (life)
        {
            case 6:
            case 5:
                Phase1();
                break;
            case 4:
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

    }

    private void Phase2()
    {

    }

    private void Phase1()
    {

    }

    protected override void OnTakeDamage(string tag)
    {

        if (tag == "Sabre")
        {
            SfxManager.Instance.PlaySaberInBoss();
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
