using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class CharAnimation : MonoBehaviour
{
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsSlide = Animator.StringToHash("IsSlide");
    private static readonly int IsTwoJump = Animator.StringToHash("IsTwoJump");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    protected Animator animator;
    private Renderer characterRenderer;
    private Material characterMaterial;
    private bool isDamageActive = false;

    public float BlinkDamageColor = 0.1f;
    private Coroutine damageCoroutine;

    private Color originalColor;
    private float blinkAlpha = 0f;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterRenderer = GetComponentInChildren<Renderer>();
        characterMaterial = characterRenderer.material;

        if (animator == null)
        {
            Debug.LogError("Animator is null");
        }

        originalColor = characterMaterial.color;
    }

    public void Damage()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }

        isDamageActive = true;
        damageCoroutine = StartCoroutine(DamageBlink());
        animator.SetBool(IsDamage, true);
    }

    private IEnumerator DamageBlink()
    {
        while (isDamageActive)
        {
            blinkAlpha = Mathf.Lerp(1f, 0f, Mathf.PingPong(Time.time / BlinkDamageColor, 1f));
            characterMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, blinkAlpha);
            yield return null;
        }
    }

    public void OffDamage()
    {
        isDamageActive = false;

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }

        animator.SetBool(IsDamage, false);
        characterMaterial.color = originalColor;
    }

    public void Jump()
    {
        animator.SetBool(IsJump, true);
    }

    public void TwoJump()
    {
        animator.SetBool(IsTwoJump, true);
    }

    public void OffJump()
    {
        animator.SetBool(IsJump, false);
        animator.SetBool(IsTwoJump, false);
    }

    public void Slide()
    {
        animator.SetBool(IsSlide, true);
    }

    public void OffSlide()
    {
        animator.SetBool(IsSlide, false);
    }

    public void Dead()
    {
        characterMaterial.color = Color.red;
        animator.SetBool(IsDead, true);
    }
}
