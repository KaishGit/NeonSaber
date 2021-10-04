using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed = 5f;
    public SpriteRenderer _SpriteRenderer; 
    public Material PlayerBullet;
    public int hp = 1;

    [HideInInspector]
    public Vector3 dir;

    private void Start()
    {
        rb2D.velocity = dir;
        SfxManager.Instance.PlayShotAttack();
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
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Sabre"))
        {
            SfxManager.Instance.PlayShotInSaber();
            _SpriteRenderer.material = PlayerBullet;
            gameObject.tag = "SabreBullet";
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            SfxManager.Instance.PlayShotInShield();
            _SpriteRenderer.material = PlayerBullet;
            gameObject.tag = "SabreBullet";
        }
        else if (collision.gameObject.CompareTag("Monster") 
            || collision.gameObject.CompareTag("Boss"))
        {
            if(gameObject.tag == "SabreBullet")
            {
                Destroy(gameObject);
            }          
        }
    }
}
