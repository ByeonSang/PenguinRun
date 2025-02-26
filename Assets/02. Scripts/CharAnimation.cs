using UnityEngine;

public class CharAnimation : MonoBehaviour
{
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsSlide = Animator.StringToHash("IsSlide");
    private static readonly int IsTwoJump = Animator.StringToHash("IsTwoJump");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();        
        if (animator == null)
        {
            Debug.LogError("Animator is null");
        }
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }    

    public void OffDamage()
    {
        animator.SetBool(IsDamage, false);
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
        animator.SetBool(IsTwoJump,false);
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
        animator.SetBool(IsDead, true);
    }
}
