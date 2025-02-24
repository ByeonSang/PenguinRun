using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharAnimation charAnimation;
    protected Rigidbody2D _rigidbody;

    public float JumpForce = 10f;
    public float CharacterHP = 100f;

    public bool isDead = false;
    float deathCooldown = 0;

    private bool isJumping = false;
    private bool isGround = true;
    private int jumpCount = 0;


    private void Start()
    {
        //gameManager = GameManager.Instance;

        charAnimation = GetComponent<CharAnimation>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (charAnimation == null)
            Debug.LogError("Animator is null");

        if (_rigidbody == null)
            Debug.LogError("Rigidbody is null");

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
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                Jump();
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
        //else if (jumpCount < 2)
        //{
        //    velocity.y += JumpForce;
        //    isJumping = false;
        //    jumpCount++;
        //    Debug.Log("더블점프");
        //}

    }

    protected void Jump()
    {        
        isJumping = true;

        jumpCount++;

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
        //transform.position = new Vector2(transform.position.x, transform.position.y + JumpForce);
        

        if (charAnimation != null)
        {
            if (jumpCount == 1)
            {
                Debug.Log("1단 점프");
                charAnimation.Jump();
            }
            else if (jumpCount == 2)
            {
                Debug.Log("2단 점프");
                charAnimation.TwoJump();
            }
        }
        
        isGround = false;
        Invoke(nameof(ResetJump), 3f);
    }

    protected void ResetJump()
    {
        isJumping = false;
        charAnimation.OffJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;


        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isJumping = false;
            jumpCount = 0;
        }

        //if (CharacterHP > 0)
        //{
        //    CharacterHP -= 20f;
        //    if (charAnimation != null)
        //    {
        //        charAnimation.Damage();
        //    }
        //}
        //else
        //{
        //    isDead = true;
        //    charAnimation.Dead();
        //    if (charAnimation != null)
        //    {
        //        deathCooldown = 1f;
        //    }       
        //}

        //gameManager.GameOver();
    }
}
