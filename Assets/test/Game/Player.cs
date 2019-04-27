using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject target;

    int airJumpAmount;
    Rotation rotation;
    Move move;
    Gravity gravity;
    Jump jump;
    Guard guard;
    Attack attack;
    Damage damage;
    Step step;
    Wait wait;

    Vector3 Input_dir;

    public Vector3 diff;
    public Vector3 beforePosition;

    public enum Status
    {
        idle, jumpping, guarding, attacking, stepping, damaged, waiting, Down,
    }


    public Status player_status;
    public bool isGrounded;
    public bool[] isHit_against_theWall = new bool[4];
    public bool isInvincible;
    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        rotation = new Rotation(transform, playerData.baseData.cam);
        move = new Move(transform, playerData);
        jump = new Jump();
        gravity = new Gravity(transform, playerData);
        guard = new Guard(transform, playerData.guardData);
        attack = new Attack(playerData.usedAttacks, playerData.baseData.animator, transform);
        damage = new Damage(transform, playerData);
        step = new Step(transform, playerData);

        wait = new Wait();
    }
    private void Start()
    {
        move.Start(playerData.baseData.animationNames[1]);
        jump.Start(playerData.baseData.gravityScale,
                   playerData.jumpData.jumpPower, false);

    }

    float coll_radius = 0.5f;
    Vector3 coll_origin;


    private void Update()
    {

        float sphere_length = 0.1f;


        beforePosition = transform.position;

        if (GetGrounded(transform.position + transform.up * coll_radius
            , coll_radius, -Vector3.up, sphere_length))
        {

            if (!isGrounded)
            {

                if (player_status == Status.idle)
                {
                    isGrounded = true;
                    wait.Set(20, 0); //着地硬直

                    player_status = Status.waiting;
                }

            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //isGroundedの方向と同じだとガタガタなるので　同じなら判定しない

        
        if (transform.forward != -Vector3.up)
            isHit_against_theWall[0] = GetGrounded(coll_origin, coll_radius, transform.forward, 0.1f);
        if (-transform.forward != -Vector3.up)
            isHit_against_theWall[1] = GetGrounded(coll_origin, coll_radius, -transform.forward, 0.1f);
        if (-transform.right != -Vector3.up)
            isHit_against_theWall[2] = GetGrounded(coll_origin, coll_radius, -transform.right, 0.1f);
        if (transform.right != -Vector3.up)
            isHit_against_theWall[3] = GetGrounded(coll_origin, coll_radius, transform.right, 0.1f);

        if (Input.GetKey(playerData.baseData.InputKeys[0]))
        {
            Input_dir = new Vector3(Input_dir.x, 0, 1);
        }
        else if (Input.GetKey(playerData.baseData.InputKeys[1]))
        {
            Input_dir = new Vector3(Input_dir.x, 0, -1);
        }
        else
        {
            Input_dir = new Vector3(Input_dir.x, 0, 0);
        }
        if (Input.GetKey(playerData.baseData.InputKeys[2]))
        {
            Input_dir = new Vector3(-1, 0, Input_dir.z);
        }
        else if (Input.GetKey(playerData.baseData.InputKeys[3]))
        {
            Input_dir = new Vector3(1, 0, Input_dir.z);
        }
        else
        {
            Input_dir = new Vector3(0, 0, Input_dir.z);
        }


        GetStatus();

        Action();

        coll_origin = transform.position + Vector3.up * coll_radius;

        diff = transform.position - beforePosition;
        if(diff.sqrMagnitude > coll_radius + 0.1)
        SURINUKE(coll_origin, coll_radius, 0.1f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(coll_origin, coll_radius);
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        if (name == "Knight")
        {
            GUILayout.BeginArea(new Rect(0, Screen.height / 2, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.Label(name);
            GUILayout.Label("isGrounded:" + isGrounded);
            GUILayout.Label("state:" + player_status);
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        else
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.Label(name);
            GUILayout.Label("isGrounded:" + isGrounded);
            GUILayout.Label("state:" + player_status);
            GUILayout.EndVertical();
            GUILayout.EndArea();

        }
    }

    void Action()
    {

        switch (player_status)
        {
            case Status.idle:
                isInvincible = false;
                rotation.Update(Input_dir);
                move.Update(Input_dir, isGrounded, isHit_against_theWall);
                gravity.Update(isGrounded);
                break;
            case Status.jumpping:
                gravity.Update(true);
                jump.Jumpping(this.transform);
                move.Update(Input_dir, false, isHit_against_theWall);
                rotation.Update(Input_dir);
                if (jump.isjumpping == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.guarding:
                damage.UPFukitobi(isHit_against_theWall);
                damage.SIDEFukitobi(isHit_against_theWall);
                if (guard.transitionProperty == Guard.Transition.Guarding)
                {
                    if (Input_dir.magnitude != 0)
                    {
                        guard.gobj.SetActive(false);
                        step.Start(Input_dir, true);
                        GetStatus(Status.stepping);
                        break;
                    }
                }
                guard.Guarding();
                gravity.Update(isGrounded);
                if (guard.isGuarding == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.attacking:

                attack.Attacking();
                attack.isGrounded = isGrounded;
                if (attack.isAttacking == false)
                {
                    GetStatus(Status.idle);
                }
                if (!isGrounded)
                {
                    move.Update(Input_dir, false, isHit_against_theWall);
                }
                break;
            case Status.stepping:
                step.Stepping(isHit_against_theWall);
                isInvincible = step.invincibleProperty > 0 ? true : false;
                if (!step.isStepping)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.damaged:
                guard.gobj.SetActive(false);
                
                if (damage.Wait())
                {
                   
                    if (!isGrounded && Input.anyKeyDown) //受身
                    {

                        transform.localEulerAngles =
                            new Vector3
                            (transform.localEulerAngles.x,
                            transform.localEulerAngles.y, 0);

                        wait.Set(30, 0);
                        GetStatus(Status.waiting);
                    }

                    if (isGrounded && damage.UPFukitobi(isHit_against_theWall) && damage.SIDEFukitobi(isHit_against_theWall))
                    {
                        wait.Set(40, 0);
                        wait.Waiting();
                        if(wait.Waiting() == true)
                        {
                            transform.localEulerAngles = new Vector3
                            (transform.localEulerAngles.x,
                            transform.localEulerAngles.y, 0);

                            GetStatus(Status.idle);
                        }
                    }

                }
                else
                {
                    wait.Set(3, 0);
                    wait.Waiting();
                    if (wait.Waiting() == true)
                    {
                        isInvincible = false;
                    }
                    else {

                        isInvincible = true;
                    }
                }
                damage.UPFukitobi(isHit_against_theWall);
                if (damage.UPFukitobi(isHit_against_theWall))
                {
                    gravity.Update(isGrounded);

                }
                

                damage.SIDEFukitobi(isHit_against_theWall);

   
                break;
            case Status.waiting:
                wait.Waiting();
                if (wait.Waiting() == true)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.Down:
                this.gameObject.SetActive(false);
                break;
        }

    }


    private void GetStatus()
    {
        if (playerData.baseData.hitPoint <= 0)
        {
            player_status = Status.Down;
        }
        else
        {
            if (isGrounded)
            {
                airJumpAmount = 0;

                if (player_status == Status.idle)
                {
                    if (Input.GetKeyDown(playerData.jumpData.keyCode))
                    {
                        jump.Start(playerData.baseData.gravityScale,
                                   playerData.jumpData.jumpPower, true);
                        GetStatus(Status.jumpping);
                    }
                    if (Input.GetKeyDown(playerData.guardData.keyCode)
                        || Input.GetKey(playerData.guardData.keyCode))
                    {
                        guard.Start();
                        GetStatus(Status.guarding);

                        if (target != null)
                        {
                            this.transform.LookAt(target.transform.position);
                        }
                    }
                    for (int i = 0; i < playerData.usedAttacks.Length; i++)
                    {
                        if (Input.GetKeyDown(playerData.usedAttacks[i].KeyCode))
                        {
                            attack.isGrounded = isGrounded;
                            attack.Start(i);
                            if (attack.attackBaceProperty.isGroundAttack)
                            {
                                if (attack.isAttacking)
                                    GetStatus(Status.attacking);
                                if (target != null)
                                {
                                    this.transform.LookAt(new Vector3(target.transform.position.x,
                                        transform.position.y, target.transform.position.z));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (player_status == Status.idle || player_status == Status.jumpping)
                {
                    if (airJumpAmount < playerData.jumpData.airJumpLimit)
                    {
                        if (Input.GetKeyDown(playerData.jumpData.keyCode))
                        {
                            airJumpAmount++;
                            jump.Start(playerData.baseData.gravityScale,
                                       playerData.jumpData.jumpPower, true);
                            GetStatus(Status.jumpping);
                        }
                    }

                    for (int i = 0; i < playerData.usedAttacks.Length; i++)
                    {
                        if (Input.GetKeyDown(playerData.usedAttacks[i].KeyCode))
                        {
                            attack.isGrounded = isGrounded;
                            attack.Start(i);

                            if (attack.isAttacking)
                            {
                                GetStatus(Status.attacking);
                                if (target != null)
                                {
                                    this.transform.LookAt(new Vector3(target.transform.position.x,
                                        transform.position.y, target.transform.position.z));
                                }
                            }
                        }
                    }
                }
            }

        }
    }
    private void GetStatus(Status status)
    {
        player_status = status;
    }

    bool GetGrounded(Vector3 origin, float radius, Vector3 ray_dir, float ray_length)
    {
        bool is_hit_ground = false;
        string ground_tag = "ground";

        // 始点における球状範囲がヒット対象範囲にならないので始点ずらして判定範囲を補正する
        Vector3 offset = -ray_dir * radius;
        ray_length += radius;

        RaycastHit raycastHit;
        float hit_dist = 0;

        Debug.DrawRay(origin, ray_dir * radius, Color.black);
        if (Physics.SphereCast(origin + offset, radius, ray_dir, out raycastHit, ray_length))
        {

            hit_dist = ray_length - raycastHit.distance;
            if (raycastHit.collider.gameObject.CompareTag(ground_tag))
            {
                is_hit_ground = true;                 
            }
            //if (Physics.Raycast(origin, ray_dir, coll_radius))
            {
                if (!raycastHit.collider.CompareTag("Weapon"))
                {
                    transform.position += hit_dist * -ray_dir;
                }
            }  
        }
        
        return is_hit_ground;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damager otherDamager = other.GetComponent<Damager>();
        if (otherDamager != null)
        {
            for (int i = 0; i < playerData.usedAttacks.Length; i++)
            {

                if (gameObject.GetInstanceID() == otherDamager.playerInstanceID)
                {
                    Debug.Log(otherDamager);
                    return;
                }

            }

            if (!isInvincible) //無敵じゃなかったらダメージ
            {


                if (otherDamager.PreventTime != 0)
                {
                    if (guard.transitionProperty == Guard.Transition.Guarding && !otherDamager.is_UnableTo_Guard)
                    {
                        if (guard.JustFrameProperty >= 0)//ジャストガード
                        {
                            guard.GuardingFrame += otherDamager.PreventTime / 4;
                        }
                        else
                        {
                            guard.GuardingFrame += otherDamager.PreventTime / 2;
                            damage.Calcurate_HitPoint(ref playerData.baseData.hitPoint, otherDamager.DamagePoint / 10);

                            damage.Start(
                                     playerData.baseData.gravityScale,
                                     playerData.baseData.airResistance,
                                     otherDamager.UpFukitobasiPower / 1.5f,
                                     otherDamager.SideFukitobsiPower / 1.5f,
                                     otherDamager.FukitobasiVector,
                                     0);

                        }
                    }
                    else
                    {
                        GetStatus(Status.damaged);
                        Debug.Log("aaa");
                        damage.Start(
                                     playerData.baseData.gravityScale,
                                     playerData.baseData.airResistance,
                                     otherDamager.UpFukitobasiPower,
                                     otherDamager.SideFukitobsiPower,
                                     otherDamager.FukitobasiVector,
                                     otherDamager.PreventTime);


                        if (otherDamager.UpFukitobasiPower > 100)
                        {
                            transform.localEulerAngles =
                                new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
                        }
                        damage.Calcurate_HitPoint(ref playerData.baseData.hitPoint, otherDamager.DamagePoint);
                    }
                }

            }
        }
    }


    void SURINUKE(Vector3 origin, float radius, float ray_length)
    {
        Debug.DrawRay(transform.position, -diff.normalized * diff.sqrMagnitude, Color.red);

        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, -diff.normalized, out raycastHit, diff.sqrMagnitude))
        {
            bool is_foward_obj = 0 <=
                Vector3.Dot(raycastHit.point - beforePosition, diff.normalized);
            if (is_foward_obj)
            {
                Debug.Log(raycastHit.collider.name);
                 transform.position =
                        raycastHit.point + -diff.normalized * (ray_length + coll_radius);
                beforePosition = transform.position;
                isGrounded = true;
            }
            else
            {
                Debug.Log(transform.position);
                Debug.Log(0 < Vector3.Dot(raycastHit.point - beforePosition, diff.normalized));
                Debug.Log(Vector3.Dot(raycastHit.point - beforePosition, diff.normalized));
            }
        }
    }
}

    


class Wait
{
    float waitFrame;
    public float waitFrameProperty
    {
        get { return waitFrame; }
    }
    bool isSet;

    public void Set(float waitFrame, float addValue)
    {
        if (addValue != 0)
        {
            this.waitFrame += addValue;
        }
        else if(!isSet)
        {
            if (waitFrame > 0)
            {
                this.waitFrame = waitFrame;
                isSet = true;
            }
        }
    }

    public bool? Waiting()
    {
        if (isSet)
        {
            waitFrame--;
            if (waitFrame <= 0)
            {
                waitFrame = 0;
                isSet = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return null;
        }
    }
}