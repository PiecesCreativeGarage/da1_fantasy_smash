using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBace{

    public bool isAttacking;

    public float AttackPoint;
    public float Frame;

    public void Update()
    {
        if (isAttacking)
        {
            Attacking();
        }
    }
    protected virtual void Attacking() { }


}
class Attack_A : AttackBace
{
    public Attack_A()
    {
        AttackPoint = 10;
        Frame = 40;
        isAttacking = true;
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
class Attack_B : AttackBace
{
    public Attack_B()
    {
        AttackPoint = 1500;
        Frame = 120;
        isAttacking = true;
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
