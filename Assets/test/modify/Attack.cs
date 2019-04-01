using System.Collections;
using System.Collections.Generic;
using UnityEngine;





class Attack
{
    public bool isAttacking;
    public bool isGrounded;
    public Animator animator;

    Transform transform;
    AttackBace attackBace;
    public AttackBace attackBaceProperty
    {
        get { return attackBace; }
    }
    PlayerData.UsedAttackData[] Attacks;

 
    public Attack(PlayerData.UsedAttackData[] UsedAttacks, Animator animator, Transform transform)
    {
        Attacks = new PlayerData.UsedAttackData[UsedAttacks.Length];
        this.transform = transform;
        this.animator = animator;
        for (int i = 0; i < Attacks.Length; i++)
        {
            Attacks[i] = UsedAttacks[i];

        }
    }

    public void Start(int number)
    {
        SetAttack(Attacks[number].AttackNumver);
        attackBace.FukitobasiVector += transform.forward;
        attackBace.weapon = Attacks[number].Weapon;
        attackBace.player = transform.gameObject;
        attackBace.Start();

        if (!(attackBace == null))
        {
            
            isAttacking = attackBace.isGroundAttack == isGrounded;
            if (isAttacking)
            {
                if (attackBace.animationName != string.Empty)
                {
                    animator.SetTrigger(attackBace.animationName);
                }
            }
        }
    }
    void SetAttack(int Numver)
    {
        attackBace = AttackDictionary.CreateAttack(Numver);
    }
    public void Attacking()
    {

        if (attackBace.isAttacking)
        {
            attackBace.isGrounded = isGrounded;
            attackBace.Update();

        }
        else
        {
            isAttacking = false;
        }
    }
}