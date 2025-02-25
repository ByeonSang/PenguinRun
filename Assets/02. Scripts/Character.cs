using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharAnimation charAnimation;
    protected Rigidbody2D _rigidbody;
    protected CircleCollider2D _circleCollider;

    public float JumpForce = 8.5f;
    public float CharacterHP = 100f;

    public bool isDead = false;
    float deathCooldown = 0;

    private bool isJumping = false;
    private bool isSliding = false;
    private bool isGround = true;
    private int jumpCount = 0;


    private void Start()
    {
        //gameManager = GameManager.Instance;

        charAnimation = GetComponentInChildren<CharAnimation>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        if (charAnimation == null)
            Debug.LogError("Animator is null");

        if (_rigidbody == null)
            Debug.LogError("Rigidbody is null");

        if (_circleCollider == null)
            Debug.LogError("CircleCollider is null");

        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && !isSliding)
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.LeftShift) && !isJumping)
            {
                Slide();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopSlide();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        if (isGround)
        {
            isJumping = false;
        }
    }

    protected void Jump()
    {
        isJumping = true;
        isGround = false;

        jumpCount++;


        if (charAnimation != null)
        {
            if (jumpCount == 1)
            {
                charAnimation.Jump();
            }
            else if (jumpCount == 2)
            {
                charAnimation.TwoJump();
            }
        }

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);

        Invoke(nameof(ResetJump), 1.5f);
    }

    protected void ResetJump()
    {
        isJumping = false;
        charAnimation.OffJump();
    }

    private void Slide()
    {
        _circleCollider.radius = 0.5f;
        isSliding = true;
        charAnimation.Slide();
    }

    private void StopSlide()
    {
        _circleCollider.radius = 1f;
        isSliding = false;
        charAnimation.OffSlide();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;


        if (collision.gameObject.CompareTag("Background"))
        {
            isGround = true;
            isJumping = false;
            jumpCount = 0;
        }

        // 장애물 닿을시 체력 감소
        //if (collision.gameObject.CompareTag(""))
        //{
        //    if (CharacterHP > 0)
        //    {
        //        CharacterHP -= 20f;
        //        if (charAnimation != null)
        //        {
        //            charAnimation.Damage();
        //        }
        //    }
        //    else
        //    {
        //        isDead = true;                
        //        if (charAnimation != null)
        //        {
        //            charAnimation.Dead();
        //            deathCooldown = 1f;
        //        }
        //        gameManager.GameOver();
        //    }
        //}

    }
}
