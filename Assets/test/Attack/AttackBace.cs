using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{
    
    public Animator animator;

    public bool isAttacking;

    public float AttackPoint;

    public float UpFukitobasiPower;
    public float SideFukitobsiPower;

    public Vector3 FukitobasiVector;

    public float PreventTime;

    public float KeepFrame;

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
        KeepFrame = 40;
        UpFukitobasiPower = 500;
        SideFukitobsiPower = 200;
 
        isAttacking = true;
        animator.SetTrigger("Attack1");
    }
    protected override void Attacking()
    {
        KeepFrame--;
        if (KeepFrame <= 0)
        {
            isAttacking = false;
        }
    }
}
class Attack_B
{

}
