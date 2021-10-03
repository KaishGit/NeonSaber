using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberControl : MonoBehaviour
{
    public Rigidbody2D _RigidBody;
    public float _SaberSpeed;

    private Vector3 direction;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = _SaberSpeed;
        direction = new Vector3(currentSpeed, currentSpeed);
        _RigidBody.velocity = direction;
    }

    void Update()
    {
        _RigidBody.velocity = _RigidBody.velocity.normalized * currentSpeed;
    }
}
