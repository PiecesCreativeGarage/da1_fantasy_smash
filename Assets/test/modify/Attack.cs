using System.Collections;
using System.Collections.Generic;
using UnityEngine;





class Attack
{
    public bool isAttacking;

    public Animator animator;

    Transform transform;
    AttackBace attackBace;
    public AttackBace attackBaceProperty
    {
        get { return attackBace; }
    }
    Player.SetUseAttack[] Attacks;

    /// <summary>
    /// 使う攻撃を判定
    /// </summary>
    /// <param name="setUseAttacks"></param>
    /// <param name="animator"></param>
    /// <param name="transform"></param>
    public Attack(Player.SetUseAttack[] setUseAttacks, Animator animator, Transform transform)
    {
        Attacks = new Player.SetUseAttack[setUseAttacks.Length];
        this.transform = transform;
        this.animator = animator;
        for (int i = 0; i < Attacks.Length; i++)
        {
            Attacks[i] = setUseAttacks[i];

        }
    }
    public void Start(int number)
    {
        SetAttack(Attacks[number].AttackNumver);
        attackBace.FukitobasiVector += transform.forward;
        attackBace.animator = this.animator;
        attackBace.gameObject = Attacks[number].Weapon;
        attackBace.Start();

        if (!(attackBace == null))
        {
            isAttacking = true;
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
            attackBace.Update();

        }
        else
        {

            isAttacking = false;
        }
    }
}