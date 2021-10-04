using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Enemy : MonoBehaviour
{
    [Header("Enemy Config")]
    public float speed = 2f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletPivots;
    public float speedBullet = 1.5f;

    [Header("Fire")]
    public float fireRate = 1f;
    private float currentDelayFire = 0f;

    [Space(10)]
    public int fireSequence = 1;
    private int currentFire = 0;
    public float fireRateSequence = 0f;
    private float currentDelaySequence = 0f;

        
    [Header("Other")]
    public Animator anim;
    public SpriteRenderer sprRenderer;
    public bool spriteIsRight;

    public int life = 1;

    protected Vector3 dirWalk;

    protected bool canFire = true;

    private bool isDead;


    private void Start()
    {
        OnStart();
    }
    void Update()
    {
        if (canFire)
        {
            currentDelayFire += Time.deltaTime;
            if (currentDelayFire >= fireRate)
            {
                currentDelaySequence += Time.deltaTime;
                if (currentDelaySequence > fireRateSequence)
                {
                    currentDelaySequence = 0f;

                    currentFire++;
                    Vector3 result = ReferenceManager.instance.playerTransform.position - transform.position;

                    var positionTarget1 = ReferenceManager.instance.playerTransform.position + (Vector3.up * 0.5f) + (Vector3.right * 0.5f);
                    var positionTarget2 = ReferenceManager.instance.playerTransform.position + (Vector3.down * 0.5f) + (Vector3.left * 0.5f);
                    if ((result.x > 0 && result.y > 0) || (result.x < 0 && result.y < 0))
                    {
                        positionTarget1 = positionTarget1 + Vector3.left;
                        positionTarget2 = positionTarget2 + Vector3.right;
                    }

                    if (bulletPivots.childCount == 1)
                    {
                        foreach (Transform bulletPivot in bulletPivots)
                        {
                            GameObject obj = Instantiate(bulletPrefab, bulletPivot.position, Quaternion.identity, transform.parent);
                            Bullet b = obj.GetComponent<Bullet>();
                            b.dir = (ReferenceManager.instance.playerTransform.position - transform.position).normalized;
                            b.speed = speedBullet;
                        }
                    }
                    else
                    {
                        GameObject obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform.parent);
                        Bullet b = obj.GetComponent<Bullet>();
                        b.dir = (positionTarget1 - transform.position).normalized;
                        b.speed = speedBullet;

                        GameObject obj1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform.parent);
                        b = obj1.GetComponent<Bullet>();
                        b.dir = (positionTarget2 - transform.position).normalized;
                        b.speed = speedBullet;

                    }

                    if (currentFire >= fireSequence)
                    {
                        currentFire = 0;
                        currentDelayFire = 0f;
                    }
                }


            }
        }
        OnUpdate();
        OnAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sabre" || collision.tag == "SabreBullet")
        {
            if (!isDead)
            {
                OnTakeDamage(collision.tag);
            }
        }
    }

    protected virtual void OnTakeDamage(string tag) 
    {
        life--;
        if (life <= 0)
        {
            isDead = true;
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {

        Debug.Log("Death");
    }

    protected virtual void OnAnimation() 
    {

        Vector3 scaleChange = Vector3.one;

        if ((ReferenceManager.instance.playerTransform.position - transform.position).x > 0)
        {


            if (spriteIsRight)
            {
                scaleChange.x = 1;
            }
            else
            {
                scaleChange.x = -1;
            }

            transform.localScale = scaleChange;
        }
        else if ((ReferenceManager.instance.playerTransform.position - transform.position).x < 0)
        {
            if (spriteIsRight)
            {
                scaleChange.x = -1;
            }
            else
            {
                scaleChange.x = 1;
            }

            transform.localScale = scaleChange;
        }
    }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }
}
