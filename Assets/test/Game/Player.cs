﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed;
    public float jumpPower;
    public float gravity;

    public float attackPoint;

    public GameObject guardPrefab;
    public GameObject cam;
    Rotation rotation;
    Move move;
    Jump jump;
    Guard guard;
    Attack attack;

    [System.Serializable]
    public class SetUseAttack
    {
        public int AttackNumver;
        public string KeyCode;
    }

    public SetUseAttack[] AttacksInfo;

    Vector3 Input_dir;

    enum Status
    {
        idle, jumpping, guarding, attacking,
    }


    Status player_status;

    private void Awake()
    {
        rotation = new Rotation();
        move = new Move();
        jump = new Jump();
        guard = new Guard();
        attack = new Attack();
    }
    private void Start()
    {
        rotation.Start(this.cam);
        move.Start(moveSpeed);
        jump.Start(jumpPower, gravity);

        guard.Start(30, "g", guardPrefab);

        attack.Start(AttacksInfo);

    }
    private void Update()
    {
        GetStatus();
        switch (player_status)
        {
            case Status.idle:
                Input_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                move.Update(transform, Input_dir);
                rotation.Update(this.transform, Input_dir);
                jump.Update();
                guard.Update();
                attack.Update();
                break;
            case Status.jumpping:
                jump.Jumpping(this.transform);

                if (jump.isjumpping == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.guarding:
                guard.Guarding(this.transform);
                if (jump.isjumpping == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.attacking:
                this.attackPoint = attack.Attacking();
                if (attack.isAttacking == false)
                {
                    this.attackPoint = 0;
                    GetStatus(Status.idle);
                }
                break;
        }

    }
    private void GetStatus()
    {

        if (jump.isjumpping)
        {
            player_status = Status.jumpping;
        }
        if (guard.isGuarding)
        {
            player_status = Status.guarding;
        }
        if (attack.isAttacking)
        {

            player_status = Status.attacking;
        }
    }
    private void GetStatus(Status idle)
    {
        player_status = idle;
    }
    class Move
    {
        float moveSpeed;

        public void Start(float PmoveSpeed)
        {
            moveSpeed = PmoveSpeed;
        }
        public void Update(Transform transform, Vector3 input)
        {
            if (input.magnitude != 0)
                transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
        }
    }

    class Rotation
    {
        GameObject cam;
        bool use_cam;

        public void Start(GameObject Pcam)
        {
            cam = Pcam;
            if (cam != null)
            {
                use_cam = true;
            }
        }
        public void Update(Transform transform, Vector3 dir)
        {
            if (use_cam)
            {
                if (dir.z == 1)
                {
                    transform.eulerAngles = cam.transform.eulerAngles + new Vector3(transform.eulerAngles.x, (dir.x * 45), transform.eulerAngles.z);
                }
                else if (dir.z == -1)
                {
                    transform.eulerAngles = cam.transform.eulerAngles + new Vector3(transform.eulerAngles.x, (180 * dir.z) - (dir.x * 45), transform.eulerAngles.z);
                }
                else if (dir.x != 0)
                {
                    transform.eulerAngles = cam.transform.eulerAngles + new Vector3(transform.eulerAngles.x, (dir.x * 90), transform.eulerAngles.z);
                }
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }

        }
    }
    class Jump
    {
        float jumpPow;
        float gravityScale;
        float velocity;
        float dist;
        public bool isjumpping;


        public void Start(float Pjumppow, float Pgravity)
        {
            jumpPow = Pjumppow;
            gravityScale = Pgravity;
        }
        public void Update()
        {


            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity = jumpPow;
                isjumpping = true;

            }

        }
        public void Jumpping(Transform transform)
        {
            velocity -= gravityScale;
            dist = velocity * Time.fixedDeltaTime;
            transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
            if (transform.position.y < 0)
            {
                dist = 0;
                velocity = 0;
                isjumpping = false;

            }
        }
    }
    class Guard
    {
        public bool isGuarding;
        GameObject gobj;
        bool gobj_exist;
        int waitFrame_value;
        int waitFrame_for_Cal;

        string keyCode;

        public void Start(int waitFrame, string keycode_Guard, GameObject guard_Object)
        {
            waitFrame_value = waitFrame;
            keyCode = keycode_Guard;
            gobj = Instantiate(guard_Object, Vector3.zero, Quaternion.identity);
            if (gobj != null)
            {
                gobj_exist = true;
                gobj.SetActive(false);
            }
            else
            {
                gobj_exist = false;
            }
        }

        public void Update()
        {

            if (Input.GetKeyDown(keyCode))
            {
                waitFrame_for_Cal = waitFrame_value;
                if (gobj_exist)
                {
                    gobj.SetActive(true);
                }
                isGuarding = true;
            }

        }
        public void Guarding(Transform Guard_Posi)
        {
            gobj.transform.position = Guard_Posi.position;
            if (waitFrame_for_Cal > 0)
                waitFrame_for_Cal--;

            if (!Input.GetKey(keyCode))
            {
                if (waitFrame_for_Cal == 0)
                {
                    if (gobj_exist)
                    {
                        gobj.SetActive(false);
                    }
                    isGuarding = false;
                }
            }
        }
    }
    class Attack
    {
        public bool isAttacking;
        AttackBace attackBace;
        SetUseAttack[] Attacks;
        public void Start(SetUseAttack[] setUseAttacks)
        {
            Attacks = new SetUseAttack[setUseAttacks.Length];


            for (int i = 0; i < Attacks.Length; i++)
            {
                Attacks[i] = setUseAttacks[i];

            }
        }
        public void Update()
        {

            for (int i = 0; i < Attacks.Length; i++)
            {

                if (Input.GetKeyDown(Attacks[i].KeyCode))
                {

                    SetAttack(Attacks[i].AttackNumver);

                    if (!(attackBace == null))
                    {

                        isAttacking = true;
                    }

                }
            }


        }
        void SetAttack(int Numver)
        {
            switch (Numver)
            {
                default:
                    attackBace = null;
                    break;

                case 1:
                    attackBace = new Attack_A();
                    break;
                case 2:
                    attackBace = new Attack_B();
                    break;
            }
        }
        public float Attacking()
        {

            if (attackBace.isAttacking)
            {
                attackBace.Update();

            }
            else
            {
                isAttacking = false;
            }
            return attackBace.AttackPoint;
        }
    }
}
