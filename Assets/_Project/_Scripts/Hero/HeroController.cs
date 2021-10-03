using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
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

    void Start()
    {
        
    }

    void Update()
    {
        if (isDead) return;

        GetMovement();

        GetDefense();

        SetAnimation();

        CheckFlip();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        MyRigidBody.MovePosition(transform.position + Vector3.ClampMagnitude(direction, 1) * Speed * Time.fixedDeltaTime);
    }

    private void GetMovement()
    {
        if (!isDefending)
        {
            axisY = Input.GetAxis("Vertical");
            axisX = Input.GetAxis("Horizontal");
            direction.x = axisX;
            direction.y = axisY;
        }
        else
        {
            direction = Vector3.zero;
            isWalking = false;
        }
    }

    private void GetDefense()
    {
        if (!isDefending && Input.GetKeyDown(KeyCode.Space))
        {
            isDefending = true;
            maxDefenseTime = Time.time + DefenseTime;
            ShieldCollider.enabled = true;

            SfxManager.Instance.PlayShieldActive();
        }

        if(isDefending && Time.time >= maxDefenseTime)
        {
            isDefending = false;
            ShieldCollider.enabled = false;
            MyAnimator.Play("Hero_Idle");
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
}
