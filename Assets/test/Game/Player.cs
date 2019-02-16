using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerData playerData;
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
        gravity = new Gravity(transform);
        guard = new Guard(transform, playerData.guardData);
        attack = new Attack(playerData.usedAttacks, playerData.baseData.animator, transform);
        damage = new Damage(playerData.baseData.animator, transform);
        step = new Step(transform, playerData);

        wait = new Wait();
    }
    private void Start()
    {
        move.Start(playerData.baseData.animationNames[1]);
        jump.Start(playerData.baseData.gravityScale,
                   playerData.jumpData.jumpPower, false);
        gravity.gravityScale = playerData.baseData.gravityScale;

    }

    float coll_radius = 0.5f;
    Vector3 coll_origin;
    private void Update()
    {
        coll_origin = transform.position + Vector3.up * coll_radius;
        GetStatus();
        if (GetGrounded(coll_origin, coll_radius, -transform.up, 0.1f))
        {
            if (!isGrounded)
            {
                if (player_status == Status.idle)
                {
                    wait.Set(20, 0); //着地硬直
                    player_status = Status.waiting;
                }
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }

        isHit_against_theWall[0] = GetGrounded(coll_origin, coll_radius, transform.forward, 0.1f);
        isHit_against_theWall[1] = GetGrounded(coll_origin, coll_radius, -transform.forward, 0.1f); isHit_against_theWall[0] = GetGrounded(coll_origin, coll_radius, transform.forward, 0.1f);
        isHit_against_theWall[2] = GetGrounded(coll_origin, coll_radius, -transform.right, 0.1f);
        isHit_against_theWall[3] = GetGrounded(coll_origin, coll_radius, transform.right, 0.1f);

        Input_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

         Action();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(coll_origin, coll_radius);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("isGrounded:" + isGrounded);
        GUILayout.Label("isHit_against_theWall:" + isHit_against_theWall);
        GUILayout.Label("state:"+player_status);
        GUILayout.EndVertical();
    }

    void Action()
    {
        switch (player_status)
        {
            case Status.idle:

                move.Update(Input_dir, isGrounded, isHit_against_theWall);
                rotation.Update(Input_dir);
                gravity.Update(isGrounded, playerData.baseData.gravityScale);
                break;
            case Status.jumpping:
                jump.Jumpping(this.transform);
                move.Update(Input_dir, false, isHit_against_theWall);
                rotation.Update(Input_dir);
                if (jump.isjumpping == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.guarding:
                damage.UPFukitobi();
                damage.SIDEFukitobi();
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

                if (guard.isGuarding == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.attacking:

                attack.Attacking();
                if (attack.isAttacking == false)
                {
                    GetStatus(Status.idle);
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
                if (isGrounded && damage.UPFukitobi()) //Down
                {
                    if (Input.anyKeyDown)
                    {
                        transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,
                                                            this.transform.eulerAngles.y,
                                                            0);
                        wait.Set(100f, 100f);
                        GetStatus(Status.waiting);
                    }
                }
                else
                {
                    damage.Wait();
                    if (damage.Wait())
                    {
                        if (Input.anyKeyDown) //受身
                        {
                            wait.Set(30, 0);
                            GetStatus(Status.waiting);
                        }

                    }
                    damage.UPFukitobi();
                    if (damage.UPFukitobi())
                    {
                        gravity.Update(isGrounded, playerData.baseData.gravityScale);
                    }
                }
                damage.SIDEFukitobi();

                

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
                if (player_status == Status.idle)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        jump.Start(playerData.baseData.gravityScale,
                                   playerData.jumpData.jumpPower, true);
                        GetStatus(Status.jumpping);
                    }
                    if (Input.GetKeyDown(KeyCode.G) || Input.GetKey(KeyCode.G))
                    {
                        guard.Start();
                        GetStatus(Status.guarding);
                    }
                    for (int i = 0; i < playerData.usedAttacks.Length; i++)
                    {
                        if (Input.GetKeyDown(playerData.usedAttacks[i].KeyCode))
                        {
                            attack.Start(i);
                            GetStatus(Status.attacking);
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

    bool GetGrounded(Vector3 origin,float radius, Vector3 ray_dir, float ray_length)
    {
        bool is_hit_ground = false;
        string ground_tag = "ground";

        // 始点における球状範囲がヒット対象範囲にならないので始点ずらして判定範囲を補正する
        Vector3 offset = -ray_dir * radius;
        ray_length += radius;

        RaycastHit raycastHit;
        if(Physics.SphereCast(origin + offset, radius, ray_dir, out raycastHit, ray_length))
        {
            is_hit_ground |= raycastHit.collider.gameObject.CompareTag(ground_tag);
            float hit_dist = ray_length - raycastHit.distance;
            // 地面にめり込んだ分押し戻す
            transform.position += hit_dist * -ray_dir;
        }
        return is_hit_ground;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInvincible) //無敵じゃなかったらダメージ
        {
            if (other.GetComponent<Damager>() != null)
            {
                Damager otherDamager = other.GetComponent<Damager>();

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
                    damage.Start(
                                 playerData.baseData.gravityScale,
                                 playerData.baseData.airResistance,
                                 otherDamager.UpFukitobasiPower,
                                 otherDamager.SideFukitobsiPower,
                                 otherDamager.FukitobasiVector,
                                 otherDamager.PreventTime);

                    damage.Calcurate_HitPoint(ref playerData.baseData.hitPoint, otherDamager.DamagePoint);
                }
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
        if (isSet && addValue != 0)
        {
            this.waitFrame += addValue;
        }
        else
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