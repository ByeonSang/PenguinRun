using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float time;
    private float OriginRadius;

    [Header("Player info")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float twoJumpForce;
    [SerializeField] private float slideRadius;
    [SerializeField] private float godDuration;

    [Header("DetectionCheck info")]
    [SerializeField] private float checkLength;
    [SerializeField] private LayerMask whatIsGround;

    // ����
    public Vector2 Velocity { get => rigid.velocity; }

    #region Component
    private Animator animator;
    private Rigidbody2D rigid;
    private CircleCollider2D col;
    private SpriteRenderer spr;
    #endregion

    #region State
    private StateMachine stateMachine;
    public PlayerState currentState;
    public PlayerState landingState;
    public PlayerState moveState;
    public PlayerState airState;
    public PlayerState slideState;
    #endregion

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        spr = GetComponentInChildren<SpriteRenderer>();

        stateMachine = new StateMachine(this);
        landingState = new LandingState(this, stateMachine, animator, "IsGround");
        moveState = new MoveState(this, stateMachine, animator, "Move");
        airState = new AirState(this, stateMachine, animator, "Air");
        slideState = new SlideState(this, stateMachine, animator, "Slide");
    }


    private void Start()
    {
        // �ʱ�ȭ
        stateMachine.Initialize();

        OriginRadius = col.radius;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        // �ִϸ��̼� �ݺ� ����
        currentState.Update();
    }

    public void Jump()
    {
        rigid.velocity = Vector3.up * jumpForce;
    }

    public void TwoJump()
    {
        rigid.velocity = Vector3.up * twoJumpForce;
    }

    public void Slide()
    {
        col.radius = slideRadius;
    }

    public void SlideExit()
    {
        col.radius = OriginRadius;
    }

    public bool GroundDetection() // �� ���� üũ
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkLength, whatIsGround);
        return (hit.collider != null);
    }

    // �浹��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (GroundDetection() && time <= 0) // ������������ �ǰݾִϸ��̼� ����
                animator.SetTrigger("Hit");

            StartCoroutine(GodMode());
        }
    }

    private IEnumerator GodMode()
    {
        time = godDuration;
        while(time > 0)
        {
            float alpha = Mathf.Clamp(Mathf.PingPong(time*20f, 1f), 0.5f, 1f);
            spr.color = new Color(spr.color.r, spr.color.r, spr.color.b, alpha);
            yield return null;
        }
        spr.color = new Color(spr.color.r, spr.color.r, spr.color.b, 1f); // �������
    }

    private void OnDrawGizmos() // ������ â���� ��ο� ����� Ȱ��
    {
        Color origin = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkLength);
        Gizmos.color = origin;
    }
}
