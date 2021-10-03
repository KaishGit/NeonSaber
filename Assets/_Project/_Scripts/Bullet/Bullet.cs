using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private Transform player;

    public float speed = 5f; 

    private Vector3 dir;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dir = (player.position - transform.position).normalized;
    }



    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
