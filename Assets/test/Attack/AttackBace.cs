using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{

    public Animator animator;

    public bool isAttacking;

    public float AttackPoint;
    public float Frame;

    public void Start()
    {
        SetAttack();
    }

    public void Update()
    {
        if (isAttacking)
        {
            Attacking();
        }
    }

    protected virtual void Attacking() { }

    protected virtual void SetAttack() { }
}
class Attack_A : AttackBace
{
    protected override void SetAttack()
    {
        AttackPoint = 10;
        Frame = 40;
        isAttacking = true;
        animator.SetTrigger("Attack1");
    }
    protected override void Attacking()
    {
        Frame--;
        if (Frame <= 0)
        {
            isAttacking = false;
        }
    }
}
class Attack_B
{

}
