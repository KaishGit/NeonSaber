using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    [Header("Configs")]
    public float speed = 2f;
    public float walkDistance = 2f;
    public float changeDistance = 0.1f;
    
    [Range(0, 360f)]
    public float angle = 0f;

    Vector3 startPosition;
    Vector3 destination;

    bool isRight;

    // Gizmos
    Vector3 startPositionGizmos;
    Vector3 point1, point2;

    private void Start()
    {
        startPosition = transform.position;
        NextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (destination - transform.position).normalized * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, destination) <= changeDistance)
        {
            NextDestination();
        }
    }

    private void NextDestination()
    {
        if (isRight)
        {
            destination = startPosition + Vector3.right * walkDistance;
            var line = startPositionGizmos + destination;
            var rotatedLine = Quaternion.AngleAxis(angle, transform.forward) * line;

            destination = rotatedLine;
        }
        else 
        {
            destination = startPosition - Vector3.right * walkDistance;
            var line = startPositionGizmos + destination;
            var rotatedLine = Quaternion.AngleAxis(angle, transform.forward) * line;

            destination = rotatedLine;
        }

        isRight = !isRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("DEATH");
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;

        if (startPositionGizmos == Vector3.zero)
            startPositionGizmos = transform.position;

        point1 = startPosition + Vector3.right * walkDistance;
        point2 = startPosition - Vector3.right * walkDistance;

        var line = startPositionGizmos + point1;
        var rotatedLine = Quaternion.AngleAxis(angle, transform.forward) * line;

        Gizmos.DrawLine(startPosition, rotatedLine);

        var line2 = startPositionGizmos + point2;
        var rotatedLine2 = Quaternion.AngleAxis(angle, transform.forward) * line2;
        Gizmos.DrawLine(startPosition, rotatedLine2);


        Gizmos.DrawSphere(rotatedLine, 0.1f);
        Gizmos.DrawSphere(rotatedLine2, 0.1f);

        UnityEditor.Handles.Label(rotatedLine2 - Vector3.up * 0.25f, "PATH");

    }
}
