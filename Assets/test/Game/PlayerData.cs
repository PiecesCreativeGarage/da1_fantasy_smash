using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [System.Serializable]
    public class BaseData
    {
        public float hitPoint;
        public float gravityScale;
        public float airResistance;

        public string[] InputKeys = new string[4]; //0 Up 1 Down, 2Left, 3 Right
        public Animator animator;
        public GameObject cam;
        public string[] animationNames = { "", "Run" };
    }

    [System.Serializable]
    public class MoveData
    {
        public float moveSpeed;
    }

    [System.Serializable]
    public class JumpData
    {
        public string keyCode;
        public float jumpPower;
        public int airJumpLimit;
    }

    [System.Serializable]
    public class GuardData
    {
        public string keyCode;
        public GameObject guardObject;
        public Vector3 plusGuardPosi, guardScale;
        public float[] transitionFrames = new float[3];
        public float justGuardFrame;

    }

    [System.Serializable]
    public class StepData
    {
        public float moveSpeed;
        public float moveDistance;
        public float invincibleFrame;
        public float[] transitionFrames = new float[3];
    }
    [System.Serializable]
    public class UsedAttackData
    {
        public int AttackNumver;
        public string KeyCode;
        public GameObject Weapon;
    }

    public BaseData baseData;
    public MoveData moveData;
    public JumpData jumpData;
    public GuardData guardData;
    public StepData stepData;
    public UsedAttackData[] usedAttacks;

}
