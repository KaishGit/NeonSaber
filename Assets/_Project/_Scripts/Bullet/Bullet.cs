using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private Transform player;

    public float speed = 5f; 

    public Vector3 dir;

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
