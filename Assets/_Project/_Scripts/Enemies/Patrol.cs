using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy
{
    [Header("Patrol")]
    [Range(0f, 20f)]
    public float walkDistance = 2f;
    public float changeDistance = 0.1f;
    [Range(0, 360f)]
    public float angleWalk = 0f;

    Vector3 destination;

    bool isReturnStart;

    public float delayStop = 1f;
    private float currentTimeStop = 0f;
    private bool isStop = true;

    protected override void OnStart()
    {
        NextDestination();
    }

    protected override void OnUpdate()
    {
        if (isStop)
        {
            currentTimeStop += Time.deltaTime;
            if (currentTimeStop >= delayStop)
            {
                currentTimeStop = 0f;
                NextDestination();
                isStop = false;
            }
            return;
        }

        dirWalk = (destination - transform.position).normalized;
        transform.position += dirWalk * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, destination) <= changeDistance)
        {
            isStop = true;
        }
    }

    protected override void OnTakeDamage(string tag)
    {

        if (tag == "Sabre")
        {
            SfxManager.Instance.PlaySaberInMonster();
        }
        else
        {
            SfxManager.Instance.PlayShotInMonster();
        }

        base.OnTakeDamage(tag);
    }

    protected override void OnDeath()
    {
        VfxManager.Instance.PlayMonsterDeath(transform.position);
        StartCoroutine(DeathCoroutine());

        base.OnDeath();
    }

    IEnumerator DeathCoroutine()
    {
        sprRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        SfxManager.Instance.PlayDeathMonster();
        Destroy(gameObject);
    }

    protected override void OnAnimation()
    {
        anim.SetBool("isStop", isStop);
        base.OnAnimation();
    }

    private void NextDestination()
    {

        Vector3 areaScale = ReferenceManager.instance.areaTransform.localScale;
        destination = new Vector3(UnityEngine.Random.Range(-(areaScale.x/2), areaScale.x/2),
                                  UnityEngine.Random.Range(-(areaScale.y/2), areaScale.y/2));

        isReturnStart = !isReturnStart;
    }

}
