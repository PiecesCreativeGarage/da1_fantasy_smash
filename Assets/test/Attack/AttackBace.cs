using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{
    public GameObject weapon;
    public GameObject player;
    public bool isGrounded;
    public string animationName = "Attack1";

    public bool isAttacking;
    public bool isGroundAttack = true;
    public bool is_UnableTo_Guard;
    public float AttackPoint;

    public float UpFukitobasiPower;
    public float SideFukitobsiPower;

    public Vector3 FukitobasiVector;

    public float PreventTime;

    protected float starFrame, endFrame;
    public float AttackingFrame;

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
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 10;
        starFrame = 20;
        AttackingFrame = 10;
        endFrame = 40;
        PreventTime = 60;
        UpFukitobasiPower = 500;
        SideFukitobsiPower = 200;
        is_UnableTo_Guard = true;
        isAttacking = true;
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
                AttackingFrame--;
                if(AttackingFrame <= 0)
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
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 10;
        starFrame = 3;
        AttackingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isAttacking = true;
        
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
                AttackingFrame--;
                if (AttackingFrame <= 0)
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
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 10;
        starFrame = 3;
        AttackingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isAttacking = true;
    }
    protected override void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                starFrame--;
                if (starFrame <= 0)
                {
                    if(weapon.GetComponent<Damager>() == null)
                    {
                        weapon.AddComponent<Damager>();
                    }
                    damager.DamagePoint = AttackPoint;
                    damager.FukitobasiVector = FukitobasiVector;
                    damager.UpFukitobasiPower = UpFukitobasiPower;
                    damager.SideFukitobsiPower = SideFukitobsiPower;
                    damager.PreventTime = PreventTime;
                    GameObject bullet = Object.Instantiate(weapon, player.transform, true);
                    bullet.transform.position = player.transform.position;
                    bullet.transform.rotation = player.transform.rotation;
                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttackingFrame--;

                if (AttackingFrame <= 0)
                {
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

class GuardBreakAttack:AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 0;
        starFrame = 10;
        AttackingFrame = 10;
        endFrame = 60;
        UpFukitobasiPower = 0;
        SideFukitobsiPower = 0;
        PreventTime = 60;
        isAttacking = true;
        is_UnableTo_Guard = true;
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
                    damager.is_UnableTo_Guard = is_UnableTo_Guard;
                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttackingFrame--;
                if (AttackingFrame <= 0)
                {
                    damager.DamagePoint = 0;
                    damager.FukitobasiVector = Vector3.zero;
                    damager.UpFukitobasiPower = 0;
                    damager.SideFukitobsiPower = 0;
                    damager.PreventTime = 0;
                    damager.is_UnableTo_Guard = false;
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
class RemoteAttack_B : AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;
    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 10;
        starFrame = 3;
        AttackingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isAttacking = true;
    }
    protected override void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                starFrame--;
                if (starFrame <= 0)
                {
                    if (weapon.GetComponent<Damager>() == null)
                    {
                        weapon.AddComponent<Damager>();
                    }
                    damager.DamagePoint = AttackPoint;
                    damager.FukitobasiVector = FukitobasiVector;
                    damager.UpFukitobasiPower = UpFukitobasiPower;
                    damager.SideFukitobsiPower = SideFukitobsiPower;
                    damager.PreventTime = PreventTime;
                    GameObject bullet = Object.Instantiate(weapon, player.transform, true);
                    bullet.transform.position = player.transform.position;
                    bullet.transform.rotation = player.transform.rotation;
                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttackingFrame--;

                if (AttackingFrame <= 0)
                {
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
class AssaultAttack:AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 10;
        starFrame = 3;
        AttackingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isAttacking = true;
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
                AttackingFrame--;
                player.transform.position += player.transform.forward * 10 * Time.fixedDeltaTime;
                if (AttackingFrame <= 0)
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
class JumpAttack : AttackBace
{
    AssistanceAttack assistance = new AssistanceAttack();
    Damager damager;

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.weapon);
        AttackPoint = 10;
        starFrame = 3;
        AttackingFrame = 50;
        endFrame = 40;
        UpFukitobasiPower = 300;
        SideFukitobsiPower = 200;
        PreventTime = 3;
        is_UnableTo_Guard = false;
        isGroundAttack = false;
        isGroundAttack = false;
        isAttacking = true;
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
                player.transform.position += new Vector3(0, -3 * Time.fixedDeltaTime);

                if (isGrounded)
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
