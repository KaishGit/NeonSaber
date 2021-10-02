using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Enemy : MonoBehaviour
{
    [Header("Enemy Config")]
    public float speed = 2f;

    [Header("Bullet")]
    public GameObject bulletPrefab;

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


    protected Vector3 dirWalk;

    private void Start()
    {
        OnStart();
    }
    void Update()
    {
        currentDelayFire += Time.deltaTime;
        if (currentDelayFire >= fireRate)
        {
            currentDelaySequence += Time.deltaTime;
            if (currentDelaySequence > fireRateSequence)
            {
                currentDelaySequence = 0f;

                currentFire++;
                Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform.parent);

                if (currentFire >= fireSequence)
                {
                    currentFire = 0;
                    currentDelayFire = 0f;
                }
            }
            

        }
        OnUpdate();
        OnAnimation();
    }

    protected virtual void OnAnimation() 
    {
        if (dirWalk.x > 0)
        {
            if (spriteIsRight)
            {
                sprRenderer.flipX = false;
            }
            else
            {
                sprRenderer.flipX = true;
            }

        }
        else if (dirWalk.x < 0)
        {
            if (spriteIsRight)
            {
                sprRenderer.flipX = true;
            }
            else
            {
                sprRenderer.flipX = false;
            }
        }
    }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }
}
