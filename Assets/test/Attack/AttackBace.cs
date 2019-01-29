using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{
    
    public Animator animator;

    public bool isAttacking;
    public bool is_UnableTo_Guard;
    public float AttackPoint;

    public float UpFukitobasiPower;
    public float SideFukitobsiPower;

    public Vector3 FukitobasiVector;

    public float PreventTime;

    protected float starFrame, endFrame;
    public float AttakingFrame;

    public enum Transition
    {
        Start, Attacking, End,
    }
    protected Transition transition;
    public Transition transitionProperty
    {
        get { return transition; }
    }

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

    protected virtual void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                break;
            case Transition.Attacking:
                break;
            case Transition.End:
                break;

        }
    }

    protected virtual void SetAttack() { }
}
class Attack_A : AttackBace
{
    protected override void SetAttack()
    {
        AttackPoint = 10;
        starFrame = 20;
        AttakingFrame = 3;
        endFrame = 40;
        UpFukitobasiPower = 500;
        SideFukitobsiPower = 200;
        is_UnableTo_Guard = true;
        isAttacking = true;
        animator.SetTrigger("Attack1");
    }
    protected override void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                starFrame--;
                if(starFrame <= 0)
                {
                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttakingFrame--;
                if(AttakingFrame <= 0)
                {
                    transition = Transition.End;
                }
                break;
            case Transition.End:
                endFrame--;
                if(endFrame <= 0)
                {
                    isAttacking = false;
                }
                break;

        }
    }
}
class Attack_B
{

}
