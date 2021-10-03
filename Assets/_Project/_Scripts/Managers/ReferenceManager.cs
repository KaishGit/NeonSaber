using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public Transform areaTransform { get; private set; }
    public Transform playerTransform { get; private set; }

    public static ReferenceManager  instance;

    private void Awake()
    {
        instance = this;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        areaTransform = GameObject.FindGameObjectWithTag("Area").transform;
    }
}
