using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    protected CharAnimation charAnimation;
    protected Rigidbody2D _rigidbody;
    protected CircleCollider2D _circleCollider;
    protected SpriteRenderer _spriteRenderer;
    public Level currentLevel;

    // ���� ����
    public bool isDead = false;
    float deathCooldown = 0;

    // ���� ����
    public float JumpForce = 7.5f;
    private bool isJumping = false;
    private int jumpCount = 0;
    private bool isGround = true;
    public Button JumpButton;

    // �����̵� ����
    private bool isSliding = false;
    public Button SlideButton;

    // �ǰ� ����
    private bool isInvincible = false; // ���� ����
    public float invincibleDuration = 2f; // ���� ���� �ð�
    private float invincibleTime = 0f;    // ���� ���� �ð�
    private float colliderRadius;

    // ü�� ����
    public Slider HealthSlider;
    public float MaxHealth = 100f;
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.Instance.GameOver();
            }
        }
    }

    // �߷� ����
    public float GravityTime = 0f;
    public float GravitySpeed = 0.5f;

    //���ǵ� ������ ���� �Һ���
    public bool isSpeeding = false;

    Color originColor = Color.white;

    private void Start()
    {
        charAnimation = GetComponentInChildren<CharAnimation>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();


        if (charAnimation == null)
            Debug.LogError("Animator is null");

        if (_rigidbody == null)
            Debug.LogError("Rigidbody is null");

        if (_circleCollider == null)
            Debug.LogError("CircleCollider is null");

        CurrentHealth = MaxHealth;
        UpdateHpBar();

        _rigidbody.gravityScale = 1f;
        JumpForce = 7.5f;

        #region colorChange

        if (PlayerPrefs.HasKey("PlayerColor"))
        {
            string str = PlayerPrefs.GetString("PlayerColor");
            float[] colorValue = str.Split('/').Select(s => float.Parse(s)).ToArray();
            originColor = new Color(colorValue[0], colorValue[1], colorValue[2], colorValue[3]);
        }
        else
        {
            originColor = Color.white;
        }

        #endregion

        JumpButton.onClick.AddListener(JumpButtonClick);
        SlideButton.onClick.AddListener(SlideButtonClick);

        colliderRadius = _circleCollider.radius;
    }

    private void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.Instance.Restart();
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

            // �ǰ� �� 2�� �� ��������
            if (isInvincible && (Time.time - invincibleTime) >= invincibleDuration)
            {
                isInvincible = false;
            }
        }

        if (GravityTime >= 10f)
        {
            _rigidbody.gravityScale += GravitySpeed;
            JumpForce += GravitySpeed * 2f;
            GravityTime = 0;
        }
        GravityTime += 1f * Time.deltaTime;

        CurrentHealth -= 1f * Time.deltaTime;
        UpdateHpBar();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        if (isGround)
        {
            isJumping = false;
        }
    }

    private void LateUpdate()
    {
        _spriteRenderer.color = originColor;
    }

    protected void Jump()
    {
        AudioManager.Instance.PlaySFX("Jump");
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
        Invoke(nameof(ResetJump), 1f);
    }

    protected void ResetJump()
    {
        if (isGround)
        {
            isJumping = false;
        }
        charAnimation.OffJump();
    }

    private void Slide()
    {
        SlideSound();
        _circleCollider.radius = 0.5f;
        isSliding = true;
        charAnimation.Slide();
    }

    private void StopSlide()
    {
        _circleCollider.radius = colliderRadius;
        isSliding = false;
        charAnimation.OffSlide();
    }

    private void SlideSound()
    {
        if (!isSliding)
        {
            AudioManager.Instance.PlaySFX("Sliding");
        }
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

        if (collision.gameObject.CompareTag("Cliff"))
        {
            currentHealth = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����۰� ������ ������ ���
        if (collision.CompareTag("Item"))
        {
            BaseItem item = collision.gameObject.GetComponent<BaseItem>();
            item.Use();
            if (item.ItemID == 2)
            {
                Debug.Log("���ǵ� ������ ����");
                return;
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

        // ��ֹ� ������ ü�� ����
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isSpeeding)
            {
                //���ǵ� ����� ���� �ε����� ��ֹ��� ��Ȱ��ȭ ��Ŵ
                collision.gameObject.SetActive(false);
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
                    currentLevel.obstacles.Add(collision.gameObject);
                return;
            }

            if (!isInvincible)
            {
                if (CurrentHealth > 0)
                {
                    TakeDamage(20);
                    //�������� ������ �޺�üĿ�� �ݶ��̴��� ��Ȱ��ȭ ��Ŵ
                    if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
                    {
                        ComboChecker comboChecker = collision.GetComponentInChildren<ComboChecker>();
                        currentLevel.comboCheckers.Add(comboChecker);
                        comboChecker.col.enabled = false;

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
                }
            }
        }        
    }

    // ������ �Ծ��� ��
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateHpBar();
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.currentCombo = 0;
        }

        if (charAnimation != null)
        {
            charAnimation.Damage();
        }

        Invoke(nameof(StopDamageAnimation), 0.5f);
    }

    public void StopDamageAnimation()
    {
        charAnimation.OffDamage();
    }

    // ü��ȸ��
    public void Heal(float healAmout)
    {
        CurrentHealth += healAmout;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateHpBar();
    }

    // HP Bar ������Ʈ
    private void UpdateHpBar()
    {
        HealthSlider.value = CurrentHealth / MaxHealth;
        if (CurrentHealth <= 0)
        {
            isDead = true;
            charAnimation.Dead();
            deathCooldown = 1f;
        }
    }

    // ���� ��ưŬ��
    void JumpButtonClick()
    {
        if (jumpCount < 2)
        {
            if (isSliding)
            {
                StopSlide();
            }
            Jump();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    // �����̵� ��ưŬ��
    void SlideButtonClick()
    {
        if (!isSliding && !isJumping)
        {
            Slide();
            Invoke(nameof(StopSlide), 1f);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
