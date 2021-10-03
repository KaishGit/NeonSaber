using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    public float RotateSpeed = 5f;
    public float Speed = 1f;
    public float Radius = 0.1f;

    private Vector2 _centre;
    private float _angle;

    private Vector2 _left;
    private Vector2 _right;

    private bool isRight;

    public float changeTimer = 2f;
    private float currentTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _left = _centre - (Vector2.right * 5f);
        _right = _centre + (Vector2.right * 5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (life)
        {
            case 6:
            case 5:
                Phase1();
                break;
            case 4:
            case 3:
                Phase2();
                break;
            case 2:
            case 1:
                Phase3();
                break;
        }

        Movement();
    }

    private void Movement()
    {
        _angle -= RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }

    private void Phase1()
    {
        Radius = 0.1f;
        RotateSpeed = 1f;
    }

    private void Phase2()
    {
        Radius = Mathf.Clamp(Radius + 0.5f * Time.deltaTime, 0f, 3f);
        RotateSpeed = Mathf.Clamp(RotateSpeed + 0.5f * Time.deltaTime, 0f, 2f); 
    }


    private void Phase3()
    {

        if (!isRight && Vector2.Distance(_left, _centre) > 0.1f)
        {
            _centre -= Vector2.right * Speed * Time.deltaTime;
        }
        else if (isRight && Vector2.Distance(_right, _centre) > 0.1f)
        {
            _centre += Vector2.right * Speed * Time.deltaTime;
        }

        if (Vector2.Distance(_left, _centre) <= 0.1f)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= changeTimer)
            {
                changeTimer = 0f;
                isRight = true;
            }
        }
        else if (Vector2.Distance(_right, _centre) <= 0.1f)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= changeTimer)
            {
                changeTimer = 0f;
                isRight = false;
            }
        }
    }



}
