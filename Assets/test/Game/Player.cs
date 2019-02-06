using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;

    public float moveSpeed;
    public float jumpPower;
    public float gravityScale;
    public float airResistance;
    public float hitPoint;
    public bool isInvincible;
    public GameObject guardPrefab;
    public GameObject cam;
    public string[] animationNames = { "", "Run" };
    Rotation rotation;
    Move move;
    Gravity gravity;
    Jump jump;
    Guard guard;
    Attack attack;
    Damage damage;
    Step step;
    Wait wait;
    [System.Serializable]
    public class SetUseAttack
    {
        public int AttackNumver;
        public string KeyCode;
        public GameObject Weapon;
    }

    public SetUseAttack[] AttacksInfo;

    Vector3 Input_dir;

    public enum Status
    {
        idle, jumpping, guarding, attacking, stepping, damaged, waiting, Down,
    }


    public Status player_status;
    public bool isGrounded;
    public bool[] isHit_against_theWall = new bool[4];

    private void Awake()
    {
        rotation = new Rotation(transform, cam);
        move = new Move(transform, animator);
        jump = new Jump();
        gravity = new Gravity(transform);
        guard = new Guard(guardPrefab);
        attack = new Attack(AttacksInfo, animator, transform);
        damage = new Damage(animator, transform);
        step = new Step(transform);

        wait = new Wait();
    }
    private void Start()
    {
        move.Start(moveSpeed, airResistance, animationNames[1]);
        jump.Start(gravityScale, jumpPower, false);
        gravity.gravityScale = this.gravityScale;

    }

    float coll_radius = 0.5f;
    Vector3 coll_origin;
    private void Update()
    {
        coll_origin = transform.position + Vector3.up * coll_radius;
        GetStatus();
<<<<<<< HEAD
        if (GetGrounded(transform.position + new Vector3(0, 0.5f), 0.5f, -transform.up, 0.5f))
=======
        if (GetGrounded(coll_origin, coll_radius, -transform.up, 0.1f))
>>>>>>> 1c6aeb43c53ea0511e1e2c2c20f89673676d6fc0
        {
            if (!isGrounded)
            {
                wait.Set(10, 0); //着地硬直
                player_status = Status.waiting;
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
<<<<<<< HEAD
        isHit_against_theWall = new bool[4];
        isHit_against_theWall[0] = GetGrounded(transform.position + new Vector3(0, 1),
                                            0.5f, transform.forward, 0.1f);
        isHit_against_theWall[1] = GetGrounded(transform.position + new Vector3(0, 1),
                                            0.5f, -transform.forward, 0.1f);
        isHit_against_theWall[2] = GetGrounded(transform.position + new Vector3(0, 1),
                                            0.5f, -transform.right, 0.1f);
        isHit_against_theWall[3] = GetGrounded(transform.position + new Vector3(0, 1),
                                            0.5f, transform.right, 0.1f);
=======

        isHit_against_theWall = GetGrounded(coll_origin, coll_radius, transform.forward, 0.1f);
>>>>>>> 1c6aeb43c53ea0511e1e2c2c20f89673676d6fc0

        Input_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            Action();
    }

<<<<<<< HEAD
    void Action()
=======
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

    void GroundAction()
>>>>>>> 1c6aeb43c53ea0511e1e2c2c20f89673676d6fc0
    {
        switch (player_status)
        {
            case Status.idle:

                move.Update(Input_dir, isGrounded, isHit_against_theWall);
                rotation.Update(Input_dir);
                gravity.Update(isGrounded, gravityScale);
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
                        step.Start(20, 3, Input_dir, true);
                        GetStatus(Status.stepping);
                        break;
                    }
                }
                guard.Guarding(this.transform);

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

                damage.Wait();
                damage.UPFukitobi();
                damage.UPFukitobi();
                if (damage.UPFukitobi())
                {
                    gravity.Update(isGrounded, gravityScale);
                }
                damage.SIDEFukitobi();
                if (damage.UPFukitobi() && damage.SIDEFukitobi() && damage.Wait())
                {
                    GetStatus(Status.idle);
                }

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
        if (hitPoint <= 0)
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
                        jump.Start(gravityScale, jumpPower, true);
                        GetStatus(Status.jumpping);
                    }
                    if (Input.GetKeyDown(KeyCode.G) || Input.GetKey(KeyCode.G))
                    {
                        guard.Start();
                        GetStatus(Status.guarding);
                    }
                    for (int i = 0; i < AttacksInfo.Length; i++)
                    {
                        if (Input.GetKeyDown(AttacksInfo[i].KeyCode))
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

<<<<<<< HEAD
    bool GetGrounded(Vector3 origin, float radius, Vector3 direction, float maxdistance)
=======
    bool GetGrounded(Vector3 origin,float radius, Vector3 ray_dir, float ray_length)
>>>>>>> 1c6aeb43c53ea0511e1e2c2c20f89673676d6fc0
    {
        bool is_hit_ground = false;
        string ground_tag = "ground";

        // 始点における球状範囲がヒット対象範囲にならないので始点ずらして判定範囲を補正する
        Vector3 offset = -ray_dir * radius;
        ray_length += radius;

        RaycastHit raycastHit;
<<<<<<< HEAD
        if (Physics.SphereCast(ray.origin, radius, ray.direction, out raycastHit, maxdistance))
        {
            if (raycastHit.collider.gameObject.CompareTag("ground"))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        else
=======
        if(Physics.SphereCast(origin + offset, radius, ray_dir, out raycastHit, ray_length))
>>>>>>> 1c6aeb43c53ea0511e1e2c2c20f89673676d6fc0
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
                        damage.Calcurate_HitPoint(ref hitPoint, otherDamager.DamagePoint / 10);

                        damage.Start(
                                 gravityScale,
                                 airResistance,
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
                                 gravityScale,
                                 airResistance,
                                 otherDamager.UpFukitobasiPower,
                                 otherDamager.SideFukitobsiPower,
                                 otherDamager.FukitobasiVector,
                                 otherDamager.PreventTime);

                    damage.Calcurate_HitPoint(ref hitPoint, otherDamager.DamagePoint);
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