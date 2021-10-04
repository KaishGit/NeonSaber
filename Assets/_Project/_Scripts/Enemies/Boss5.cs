using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5 : Patrol
{
    private bool isDefending;
    public float DefenseTime;
    public Collider2D ShieldCollider;
    public SaberControl Saber;

    private float maxDefenseTime;
    private float nextDefense;

    protected override void OnUpdate()
    {
        if (!isDefending) {
            base.OnUpdate();

            if (Time.time >= maxDefenseTime && Time.time >= nextDefense)
            {
                canFire = false;
                isDefending = true;
                maxDefenseTime = Time.time + DefenseTime;
                ShieldCollider.enabled = true;

                SfxManager.Instance.PlayShieldActive();
            }
        }
        else if (Time.time >= maxDefenseTime)
        {
            canFire = true;
            isDefending = false;
            ShieldCollider.enabled = false;

            nextDefense = Time.time + Random.Range(1.5f, 7f);
        }


        anim.SetBool("isDefending", isDefending);

        

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

            if (!isInvencible)
            {
                SaberEffectManager.Instance.SetSaberByBoss(Saber);
            }
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
