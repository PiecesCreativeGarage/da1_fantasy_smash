using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{

    public Damager damager;
    public AssistanceAttack assistance = new AssistanceAttack();

    public GameObject weapon;

    protected GameObject bullet;

    public Vector3 bulletPosi;
    public Vector3 bulletRote;
    public Vector3 bulletSize;

    public Collider weaponCollider;
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
                starFrame--;
                if (starFrame <= 0)
                {
                    bullet = Object.Instantiate(weapon);
                    bullet.transform.position = bulletPosi;
                    bullet.transform.forward = bulletRote;
                    bullet.transform.localScale = bulletSize;
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

    public void GetDamagerValue(bool isIn)
    {
        if (isIn)
        {
            damager.DamagePoint = AttackPoint;
            damager.FukitobasiVector = FukitobasiVector;
            damager.UpFukitobasiPower = UpFukitobasiPower;
            damager.SideFukitobsiPower = SideFukitobsiPower;
            damager.PreventTime = PreventTime;
            damager.is_UnableTo_Guard = is_UnableTo_Guard;
        }
        else
        {
            damager.DamagePoint = 0;
            damager.FukitobasiVector = Vector3.zero;
            damager.UpFukitobasiPower = 0;
            damager.SideFukitobsiPower = 0;
            damager.PreventTime = 0;
            damager.is_UnableTo_Guard = false;
        }
    }

    protected virtual void SetAttack() { }
}

class NormalAttack : AttackBace
{

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.weapon, player);
       
        isAttacking = true;
    }
    protected override void Attacking()
    {
        base.Attacking();
        if (transition == Transition.Start && starFrame == 1)
        {
            base.GetDamagerValue(true);
        }
        if (transition == Transition.Attacking && AttackingFrame <= 1)
        {
            Object.Destroy(bullet);
        }
    }
}

public class AssistanceAttack : MonoBehaviour
{
    public void GetDamager(ref Damager storedObject, GameObject gameObject, GameObject player)
    {
        storedObject = gameObject.GetComponent<Damager>();
        storedObject.playerInstanceID = player.GetInstanceID();
    }
   
}

class Attack_A : NormalAttack
{
    protected override void SetAttack()
    {
        base.SetAttack();
        AttackPoint = 35;
        starFrame = 15;
        AttackingFrame = 12;
        endFrame = 20;
        UpFukitobasiPower = 700;
        SideFukitobsiPower = 400;
        PreventTime = 15;
        is_UnableTo_Guard = false;
        bulletPosi = player.transform.position + new Vector3(0, 2, 0) + player.transform.forward * 2;
        bulletSize = new Vector3(1, 2.5f, 2.5f);
    }
    protected override void Attacking()
    {
        base.Attacking();
    }
}
class Attack_B : NormalAttack
{
    protected override void SetAttack()
    {
        base.SetAttack();
        AttackPoint = 20;
        starFrame = 5;
        AttackingFrame = 12;
        endFrame = 6;
        UpFukitobasiPower = 450;
        SideFukitobsiPower = 300;
        PreventTime = 15;
        is_UnableTo_Guard = false;
        bulletPosi = player.transform.position + new Vector3(0, 1.5f)
            + player.transform.forward * 2f;
        bulletSize = new Vector3(3, 1, 2.5f);
    }
}
class RemoteAttack : AttackBace
{

    protected override void SetAttack()
    {
        assistance.GetDamager(ref damager, base.weapon, player);
        bulletPosi = player.transform.position + new Vector3(0, weapon.transform.localScale.y / 2);
        bulletSize = weapon.transform.localScale;
        bulletRote = player.transform.forward;
        isAttacking = true;
    }

    protected override void Attacking()
    {
        base.Attacking();
    }
}
class RemoteAttack_A: RemoteAttack
{
 
    protected override void SetAttack()
    {
        base.SetAttack();
        starFrame = 30;
        AttackingFrame = 0;
        endFrame = 15;
        is_UnableTo_Guard = false;
    }

    protected override void Attacking()
    {
        base.Attacking();
    }
}
class RemoteAttack_B : RemoteAttack
{
    protected override void SetAttack()
    {
        base.SetAttack();
        starFrame = 15;
        AttackingFrame = 0;
        endFrame = 30;
    }

    protected override void Attacking()
    {
        base.Attacking();
    }
}

class GuardBreakAttack:NormalAttack
{
    protected override void SetAttack()
    {

        base.SetAttack();
        AttackPoint = 10;
        starFrame = 5;
        AttackingFrame = 5;
        endFrame = 20;
        UpFukitobasiPower = 0;
        SideFukitobsiPower = 0;
        PreventTime = 40;
        is_UnableTo_Guard = true;
        bulletPosi = player.transform.position + new Vector3(0, 1)
            + player.transform.forward * 1.5f;
        bulletSize = new Vector3(1.5f, 1, 1.5f);
    }

    protected override void Attacking()
    {
        base.Attacking();
    }
}

class AssaultAttack:NormalAttack
{
    protected override void SetAttack()
    {
        base.SetAttack();
        AttackPoint = 25;
        starFrame = 7;
        AttackingFrame = 6;
        endFrame = 30;
        UpFukitobasiPower = 500;
        SideFukitobsiPower = 250;
        PreventTime = 25;
        is_UnableTo_Guard = false;
    }

    protected override void Attacking()
    {
        base.Attacking();

        if (bullet != null)
        {
            bullet.transform.position = bulletPosi;
            bullet.transform.localScale = bulletSize;
        }

        if (transition == Transition.Attacking)
        {
            player.transform.position 
                += player.transform.forward * 45 * Time.fixedDeltaTime;
            bulletPosi = player.transform.position;
            bulletSize = new Vector3(1.5f, 2, 2);
        }
    }

}
class JumpAttack : NormalAttack
{
    protected override void SetAttack()
    {
        base.SetAttack();
        AttackPoint = 30;
        starFrame = 5;        
        endFrame = 20;
        UpFukitobasiPower = 550;
        SideFukitobsiPower = 300;
        PreventTime = 28;
        is_UnableTo_Guard = false;
        isGroundAttack = false;      
    }
    protected override void Attacking()
    {
        switch (transition)
        {
            case Transition.Start:
                starFrame--;
                if (starFrame <= 0)
                {
                    bullet = Object.Instantiate(weapon);
                    transition = Transition.Attacking;
                }
                break;
            case Transition.Attacking:
                AttackingFrame--;
                bullet.transform.position = player.transform.position + player.transform.forward * 2;
                bullet.transform.localScale = new Vector3(1, 2, 1);
                player.transform.position += Vector3.down * 18 * Time.fixedDeltaTime;
                if (isGrounded)
                {
                    Object.Destroy(bullet);
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