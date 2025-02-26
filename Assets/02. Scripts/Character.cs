using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    protected CharAnimation charAnimation;
    protected Rigidbody2D _rigidbody;
    protected CircleCollider2D _circleCollider;

    public float JumpForce = 7.5f;

    public bool isDead = false;
    float deathCooldown = 0;

    private bool isJumping = false;
    private bool isSliding = false;
    private bool isGround = true;
    private int jumpCount = 0;

    //피격 시 무적
    private bool isInvincible = false; // 무적 상태
    public float invincibleDuration = 1.5f; // 무적 지속 시간
    private float invincibleTime = 0f;    // 무적 시작 시간

    public Slider HealthSlider;
    public float MaxHealth = 100f;
    public float CurrentHealth;

    public Button JumpButton;
    public Button SlideButton;

    public float GravityTime = 0f;
    public float GravitySpeed = 0.5f;

    //스피드 아이템 관련 불변수
    public bool isSpeeding = false;

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

        CurrentHealth = MaxHealth;
        UpdateHpBar();

        _rigidbody.gravityScale = 1f;
        JumpForce = 7.5f;

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

            // 피격 시 1.5초 후 무적해제
            if (isInvincible && (Time.time - invincibleTime) >= invincibleDuration)
            {
                isInvincible = false;
            }
        }

        if (GravityTime >= 5f)
        {
            _rigidbody.gravityScale += GravitySpeed;
            JumpForce += GravitySpeed * 2f;
            GravityTime = 0;
        }
        GravityTime += 1f * Time.deltaTime;

        CurrentHealth -= 1f * Time.deltaTime;
        UpdateHpBar();

        if (CurrentHealth <= 0 && !isDead)
        {
            isDead = true;
            charAnimation.Dead();
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

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isJumping = false;
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //아이템과 닿으면 아이템 사용
        if (collision.CompareTag("Item"))
        {
            IUseable item = collision.gameObject.GetComponent<IUseable>();
            item.Use();
            Destroy(collision.gameObject);
        }

        // 장애물 닿을시 체력 감소
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isSpeeding)
            {
                collision.gameObject.SetActive(false);
                return;
            }

            if (!isInvincible)
            {
                if (CurrentHealth > 0)
                {
                    TakeDamage(20);
                    if (charAnimation != null)
                    {
                        charAnimation.Damage();
                    }
                    isInvincible = true;
                    invincibleTime = Time.time;
                }
                else
                {
                    isDead = true;
                    if (charAnimation != null)
                    {
                        charAnimation.Dead();
                        deathCooldown = 1f;
                    }
                    //gameManager.GameOver();
                }
            }
        }
    }

    // 데미지 입었을 때
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateHpBar();
    }


    // 체력회복
    public void Heal(float healAmout)
    {
        CurrentHealth += healAmout;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateHpBar();
    }

    // HP Bar 업데이트
    private void UpdateHpBar()
    {
        HealthSlider.value = CurrentHealth / MaxHealth;
        if (CurrentHealth <= 0)
        {
            isDead = true;
            charAnimation.Dead();
        }
    }

    // 점프 버튼클릭
    void JumpButtonClick()
    {
        if (jumpCount < 2 && !isSliding)
        {
            jumpCount++;
            Jump();
        }
    }

    // 슬라이드 버튼클릭
    void SlideButtonClick()
    {
        Debug.Log("슬라이드버튼");
        if (!isSliding && !isJumping)
            Slide();

        Invoke(nameof(StopSlide), 1f);
    }
}
