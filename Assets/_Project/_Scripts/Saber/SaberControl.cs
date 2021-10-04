using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberControl : MonoBehaviour
{
    public Rigidbody2D _RigidBody;
    public float _SaberSpeed;
    public float duration;

    [HideInInspector]
    public Vector3 direction;

    void Start()
    {
        if(_RigidBody.velocity == Vector2.zero)
        {
            direction = new Vector3(_SaberSpeed, _SaberSpeed);
            _RigidBody.velocity = direction;
        }     
    }

    void Update()
    {
        _RigidBody.velocity = _RigidBody.velocity.normalized * _SaberSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            SaberEffectManager.Instance.SetSaberByBoss(this);
        }
    }
}
