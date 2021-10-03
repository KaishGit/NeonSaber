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

    Vector3 startPosition;
    Vector3 destination;

    bool isReturnStart;

    public float delayStop = 1f;
    private float currentTimeStop = 0f;
    private bool isStop = true;

    // Gizmos
    Vector3 point;

    protected override void OnStart()
    {
        startPosition = transform.position;
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

    protected override void OnAnimation()
    {
        anim.SetBool("isStop", isStop);
        base.OnAnimation();
    }

    private void NextDestination()
    {
        if (isReturnStart)
        {
            destination = startPosition;
        }
        else 
        {
            destination = Vector3.zero - Vector3.right * walkDistance;
            var line = Vector3.zero + destination;
            var rotatedLine = Quaternion.AngleAxis(angleWalk, transform.forward) * line;

            destination = startPosition + rotatedLine;
        }

        isReturnStart = !isReturnStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SABER") {
            Debug.Log("DEATH");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (!Application.isPlaying)
            startPosition = transform.position;

        point = Vector3.zero - Vector3.right * walkDistance;

        var line = Vector3.zero + point;
        var rotatedLine = Quaternion.AngleAxis(angleWalk, Vector3.forward) * line;
        Gizmos.DrawLine(startPosition, startPosition + rotatedLine);

        Gizmos.DrawSphere(startPosition, 0.1f);
        Gizmos.DrawSphere(startPosition + rotatedLine, 0.1f);

        UnityEditor.Handles.Label(startPosition + rotatedLine - Vector3.up * 0.25f, "PATH");

    }
}
