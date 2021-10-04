using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private Transform player;

    public float speed = 5f; 

    [HideInInspector]
    public Vector3 dir;

    private int hp = 1;

    private void Start()
    {
        rb2D.velocity = dir;
    }

    void Update()
    {
        rb2D.velocity = rb2D.velocity.normalized * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            hp--;
            SfxManager.Instance.PlayShotInWall();

            if(hp < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
