using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{
    public GameObject gameObject;
    public Transform playerTransform;
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

class AssistanceAttack : MonoBehaviour
{
    public void GetDamager(ref Damager storedObject, GameObject gameObject)
    {
        storedObject = gameObject.GetComponent<Damager>();
    }

}
class Attack_A : AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.gameObject);
        AttackPoint = 10;
        starFrame = 20;
        AttakingFrame = 3;
        endFrame = 40;
        PreventTime = 60;
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
                    damager.DamagePoint = AttackPoint;
                    damager.FukitobasiVector = FukitobasiVector;
                    damager.UpFukitobasiPower = UpFukitobasiPower;
                    damager.SideFukitobsiPower = SideFukitobsiPower;
                    damager.PreventTime = PreventTime;

                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttakingFrame--;

                if(AttakingFrame <= 0)
                {
                    damager.DamagePoint = 0;
                    damager.FukitobasiVector = Vector3.zero;
                    damager.UpFukitobasiPower = 0;
                    damager.SideFukitobsiPower = 0;
                    damager.PreventTime = 0;

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
class Attack_B : AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.gameObject);
        AttackPoint = 10;
        starFrame = 3;
        AttakingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isAttacking = true;
        animator.SetTrigger("Attack1");
    }
    protected override void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                starFrame--;
                if (starFrame <= 0)
                {
                    damager.DamagePoint = AttackPoint;
                    damager.FukitobasiVector = FukitobasiVector;
                    damager.UpFukitobasiPower = UpFukitobasiPower;
                    damager.SideFukitobsiPower = SideFukitobsiPower;
                    damager.PreventTime = PreventTime;

                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttakingFrame--;
                Debug.Log(AttakingFrame);
                if (AttakingFrame <= 0)
                {
                    damager.DamagePoint = 0;
                    damager.FukitobasiVector = Vector3.zero;
                    damager.UpFukitobasiPower = 0;
                    damager.SideFukitobsiPower = 0;
                    damager.PreventTime = 0;
                    
                    transition = Transition.End;
                }
                break;
            case Transition.End:
                endFrame--;
                if (endFrame <= 0)
                {
                    isAttacking = false;
                }
                break;
        }
    }
}

class RemoteAttack:AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.gameObject);
        AttackPoint = 10;
        starFrame = 3;
        AttakingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isAttacking = true;
        animator.SetTrigger("Attack1");
    }
    protected override void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                starFrame--;
                if (starFrame <= 0)
                {
                
                    damager.DamagePoint = AttackPoint;
                    damager.FukitobasiVector = FukitobasiVector;
                    damager.UpFukitobasiPower = UpFukitobasiPower;
                    damager.SideFukitobsiPower = SideFukitobsiPower;
                    damager.PreventTime = PreventTime;
                    Object.Instantiate(gameObject, playerTransform.position, playerTransform.rotation);
                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttakingFrame--;

                if (AttakingFrame <= 0)
                {
                    damager.DamagePoint = 0;
                    damager.FukitobasiVector = Vector3.zero;
                    damager.UpFukitobasiPower = 0;
                    damager.SideFukitobsiPower = 0;
                    damager.PreventTime = 0;

                    transition = Transition.End;
                }
                break;
            case Transition.End:
                endFrame--;
                if (endFrame <= 0)
                {
                    isAttacking = false;
                }
                break;
        }
    }
}
