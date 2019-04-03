using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackDictionary {
    

    static Dictionary<int, System.Type> AttackType = new Dictionary<int, System.Type>()
    {
        { 1, typeof(Attack_A)},
        { 2, typeof(Attack_B)},
        { 3, typeof(RemoteAttack_A) },
        { 4, typeof(GuardBreakAttack) },
        { 5, typeof(AssaultAttack) },
        { 6, typeof(JumpAttack) },
    };

    public static AttackBace CreateAttack(int id)
    {
        AttackBace attackBace;
        System.Type type = AttackType[id];

        attackBace = (AttackBace)System.Activator.CreateInstance(type);

        return attackBace;
    }
}
