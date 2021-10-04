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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SfxManager.Instance.PlaySaberInWall();
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            SfxManager.Instance.PlaySaberInShield();
        }
        else if (collision.gameObject.CompareTag("Sabre"))
        {
            SfxManager.Instance.PlaySaberInSaber();
        }
    }
}
