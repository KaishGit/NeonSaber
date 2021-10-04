using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    PlayerControls controls;

    public float Speed;
    public Rigidbody2D MyRigidBody;
    public Animator MyAnimator;
    public float DefenseTime;
    public Collider2D ShieldCollider;

    private bool isDead, isWalking, isDefending;

    private float axisX, axisY;
    private Vector3 direction;
    private bool flipX;
    private Vector3 newScale;
    private float maxDefenseTime;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += GetMovement;
        controls.Gameplay.Movement.canceled += GetMovement;

        controls.Gameplay.Defense.performed += GetDefense;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }


    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isDead) return;

        DefenseTimer();

        SetAnimation();

        CheckFlip();
    }

    private void DefenseTimer()
    {
        if (isDefending && Time.time >= maxDefenseTime)
        {
            isDefending = false;
            ShieldCollider.enabled = false;
            MyAnimator.Play("Hero_Idle");
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        MyRigidBody.MovePosition(transform.position + Vector3.ClampMagnitude(direction, 1) * Speed * Time.fixedDeltaTime);
    }

    private void GetMovement(InputAction.CallbackContext obj)
    {
        if (!isDefending)
        {
            Vector2 inputMovement = obj.ReadValue<Vector2>();
            axisY = inputMovement.y;
            axisX = inputMovement.x;
            direction.x = axisX;
            direction.y = axisY;
        }
        else
        {
            direction = Vector3.zero;
            isWalking = false;
        }
    }

    private void GetDefense(InputAction.CallbackContext obj)
    {
        if (!isDefending)
        {
            isDefending = true;
            maxDefenseTime = Time.time + DefenseTime;
            ShieldCollider.enabled = true;

            SfxManager.Instance.PlayShieldActive();
        }

    }

    private void SetAnimation()
    {
        if (direction == Vector3.zero && isWalking)
        {
            isWalking = false;

            MyAnimator.Play("Hero_Idle");
        }

        if (direction != Vector3.zero && !isWalking)
        {
            isWalking = true;

            MyAnimator.Play("Hero_Walk");
        }

        if (isDefending)
        {
            MyAnimator.Play("Hero_Shield");
        }
    }

    private void CheckFlip()
    {
        if (axisX > 0)
        {
            if (flipX) Flip();
        }
        else if (axisX < 0)
        {
            if (!flipX) Flip();
        }
    }

    private void Flip()
    {
        flipX = !flipX;
        newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDefending) return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDefending) return;

        if (collision.gameObject.CompareTag("Sabre"))
        {
            isDead = true;
            transform.localScale = Vector3.zero;

            VfxManager.Instance.PlayHeroDeath(transform.position);
            SfxManager.Instance.PlaySaberInHero();
            SfxManager.Instance.PlayDeathHero(0.2f);

            Debug.Log("Morri");
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Sabre"))
        {
            isDead = true;
            transform.localScale = Vector3.zero;

            VfxManager.Instance.PlayHeroDeath(transform.position);
            SfxManager.Instance.PlaySaberInHero();
            SfxManager.Instance.PlayDeathHero(0.2f);

            Debug.Log("Morri");
        }
    }
}
