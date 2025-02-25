using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    //�ǰ� �� ����
    private bool isInvincible = false; // ���� ����
    public float invincibleDuration = 1.5f; // ���� ���� �ð�
    private float invincibleTime = 0f;    // ���� ���� �ð�

    public Slider HealthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    public Button JumpButton;
    public Button SlideButton;


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

        currentHealth = maxHealth;
        UpdateHpBar();
        
        JumpButton.onClick.AddListener(JumpButtonClick);
        SlideButton.onClick.AddListener(SlideButtonClick);
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

            // �ǰ� �� 1.5�� �� ��������
            if (isInvincible && (Time.time - invincibleTime) >= invincibleDuration)
            {
                isInvincible = false;
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


        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGround = true;
        //    isJumping = false;
        //    jumpCount = 0;
        //}

        //// ��ֹ� ������ ü�� ����
        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    if (!isInvincible)
        //    {
        //        if (CharacterHP > 0)
        //        {
        //            TakeDamage(20);
        //            if (charAnimation != null)
        //            {
        //                charAnimation.Damage();
        //            }
        //            isInvincible = true;
        //            invincibleTime = Time.time;
        //        }
        //        else
        //        {
        //            isDead = true;
        //            if (charAnimation != null)
        //            {
        //                charAnimation.Dead();
        //                deathCooldown = 1f;
        //            }
        //            //gameManager.GameOver();
        //        }
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            IUseable item = collision.gameObject.GetComponent<IUseable>();
            item.Use();
            Destroy(collision.gameObject);
        }
    }

    // ������ �Ծ��� ��
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHpBar();
    }


    // ü��ȸ��
    public void Heal(float healAmout)
    {
        currentHealth += healAmout;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHpBar();
    }

    // HP Bar ������Ʈ
    private void UpdateHpBar()
    {
        HealthSlider.value = currentHealth / maxHealth;
    }

    // ���� ��ưŬ��
    void JumpButtonClick()
    {
        if (jumpCount < 2 && !isSliding)
            Jump();
    }

    // �����̵� ��ưŬ��
    void SlideButtonClick()
    {
        Debug.Log("�����̵��ư");
        if (!isSliding && !isJumping)
            Slide();

        Invoke(nameof(StopSlide),1f);
    }
}
