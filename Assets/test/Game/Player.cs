using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Animator animator;

    public float moveSpeed;
    public float jumpPower;
    public float gravityScale;
    public float airResistance;
    public float hitPoint;
    public bool isInvincible;
    public GameObject guardPrefab;
    public GameObject cam;
    public string[] animationNames = {"", "Run"};
    Rotation rotation;
    Move move;
    Gravity gravity;
    Jump jump;
    Guard guard;
    Attack attack;
    Damage damage;
    Step step;
    Wait wait;

    public Damager damager;

    [System.Serializable]
    public class SetUseAttack
    {
        public int AttackNumver;
        public string KeyCode;
    }

    public SetUseAttack[] AttacksInfo;

    Vector3 Input_dir;

    public enum Status
    {
        idle, jumpping, guarding, attacking, stepping, damaged, waiting, Down,
    }


    public Status player_status;
    public bool isGrounded;
    public bool isHit_against_theWall;

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
        damager = GetComponent<Damager>();
        wait = new Wait();
    }
    private void Start()
    {
        move.Start(moveSpeed, airResistance, animationNames[1]);
        jump.Start(gravityScale, jumpPower, false);
        gravity.gravityScale = this.gravityScale;

    }
    private void Update()
    {
        GetStatus();
        if (GetGrounded(transform.position + new Vector3(0, 0.5f), 0.5f, -transform.up, 0.1f))
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

        isHit_against_theWall = GetGrounded(transform.position + new Vector3(0, 2),
                                            1f, transform.forward, 3f);
                                        

        Input_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (isGrounded)
        {
            GroundAction();
        }
        else
        {
            AirAction();
        }
    }

    void GroundAction()
    {
        switch (player_status)
        {
            case Status.idle:
                
                move.Update(Input_dir, true, isHit_against_theWall);
                rotation.Update(Input_dir);
                gravity.Update(true, gravityScale);
                break;
            case Status.jumpping:
                jump.Jumpping(transform);
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
                if (attack.attackBaceProperty.transitionProperty == AttackBace.Transition.Attacking)
                {
                    GiveDamage(
                        attack.attackPoint,
                        attack.upfukitobasiPower,
                        attack.sidefukitobasiPower,
                        attack.fukitobasiVector,
                        attack.preventTime,
                        attack.attackBaceProperty.is_UnableTo_Guard);
                }
                if (attack.isAttacking == false)
                {
                    GiveDamage(0, 0, 0, Vector3.zero, 0, false);
                    
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
                 damage.SIDEFukitobi();
                 if (damage.UPFukitobi() && damage.SIDEFukitobi() && damage.Wait())
                 {
                      GetStatus(Status.idle);
                 }

                break;
            case Status.waiting:
                wait.Waiting();
                if(wait.Waiting() == true)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.Down:
                this.gameObject.SetActive(false);
                break;
        }
    }
    void AirAction()
    {
        switch (player_status)
        {
            case Status.idle:
               
                move.Update(Input_dir, false, isHit_against_theWall);
                rotation.Update(Input_dir);
                gravity.Update(false, gravityScale);
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
                gravity.Update(isGrounded, gravityScale);
                guard.Guarding(this.transform);
                if (guard.isGuarding == false)
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
                if (damage.UPFukitobi())
                {
                    gravity.Update(false, gravityScale);
                }
                damage.SIDEFukitobi();

                if (damage.UPFukitobi() && damage.SIDEFukitobi() && damage.Wait())
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

    bool GetGrounded(Vector3 origin,float radius, Vector3 direction, float maxdistance)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit raycastHit;
        if(Physics.SphereCast(ray.origin, radius, ray.direction, out raycastHit, maxdistance))
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
        {
            return false;
        }
    }

    void GiveDamage(
        float DamagePoint, 
        float UpFukitobasiPower, 
        float SideFukitobsiPower, 
        Vector3 FukitobasiVector,
        float PreventTime,
        bool is_UnableTo_Guard)
    {
        damager.DamagePoint = DamagePoint;
        damager.UpFukitobasiPower = UpFukitobasiPower;
        damager.SideFukitobsiPower = SideFukitobsiPower;
        damager.FukitobasiVector = FukitobasiVector;
        damager.PreventTime = PreventTime;
        damager.is_UnableTo_Guard = is_UnableTo_Guard;
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

    class Move
    {
        string animationName;
        float moveSpeed;
        float airResistance;
        Animator animator;
        Transform transform;
  
        public Move(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
        }
        public void Start(float moveSpeed, float airResistance, string animationName)
        {
            this.moveSpeed = moveSpeed;
            this.airResistance = airResistance;
            this.animationName = animationName;
        }
        public void Update(Vector3 input, bool isGrounded, bool isHit_against_theWall)
        {
            if (!isHit_against_theWall)
            {
                if (isGrounded)
                {
                    if (input.magnitude != 0)
                    {
                        animator.SetBool(animationName, true);
                        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
                    }
                    else
                    {
                        animator.SetBool(animationName, false);
                    }
                }
                else
                {
                    if (input.magnitude != 0)
                    {
                        transform.position += transform.forward * (moveSpeed - airResistance) * Time.fixedDeltaTime;
                        animator.SetBool(animationName, false);
                    }
                }
            }
        }
    }

    class Rotation
    {
        Transform transform;
        GameObject cam;
        public bool use_cam;
 
        public Rotation(Transform transform ,GameObject Pcam)
        {
            this.transform = transform;
            cam = Pcam;
            if (cam != null)
            {
                use_cam = true;
            }
        }


        public void Update(Vector3 dir)
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

    class Gravity
    {
        public float gravityScale;
        float value;
        Transform transform;
        public Gravity(Transform transform)
        {
            this.transform = transform;
      
        }
        public void Update(bool isGounded, float gravityScale)
        {
            this.gravityScale = gravityScale;
            if (isGounded)
            {
                value = 0;
            }
            else
            {
                value -= gravityScale / 15;
                transform.position += new Vector3(0, value * Time.fixedDeltaTime);
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
        
        public void Start(float gravityScale, float jumpPower, bool isJumpping)
        {
            jumpPow = jumpPower;
            this.gravityScale = gravityScale;
            velocity = jumpPow;
            isjumpping = isJumpping;
        }
        public void Jumpping(Transform transform)
        {
            velocity -= gravityScale;
            dist = velocity / 15;
            transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
            if (velocity < 0)
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
        public GameObject gobj;
        bool gobj_exist;

        public float GuardingFrame;

        float StartFrame, EndFrame, JustGuardFrame;
        public float JustFrameProperty
        {
            get { return JustGuardFrame; }
        }

        public enum Transition
        {
            Start, Guarding, End,
        }
        Transition transition;

        public Transition transitionProperty {
            get { return transition; }
        }
        public Guard(GameObject guard_Object)
        {

            if (guard_Object != null)
            {
                gobj = Instantiate(guard_Object, Vector3.zero, Quaternion.identity);
                gobj.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
                gobj_exist = true;
                gobj.SetActive(false);
            }
            else
            {
                gobj_exist = false;
            }
        }

        public void Start()
        {
            
            StartFrame = 5f; GuardingFrame = 7f; //時間を設定
            EndFrame = 4f; JustGuardFrame = 3f;

            isGuarding = true;
            transition = Transition.Start;
            

        }
        public void Guarding(Transform Guard_Posi)
        {
            gobj.transform.position = Guard_Posi.position + new Vector3(0, 1.25f);


            switch (transition) {
                case Transition.Start:
                    StartFrame--;
                    if(StartFrame <= 0)
                    {
                        transition = Transition.Guarding;
                    }
                    break;
                case Transition.Guarding:
                    if (gobj_exist)
                    {
                        gobj.SetActive(true);
                    }
                    

                    GuardingFrame--;
                    JustGuardFrame--;
                    if (!Input.GetKey(KeyCode.G) && GuardingFrame <= 0)
                    {

                        if (gobj_exist)
                        {
                            gobj.SetActive(false);
                        }
                        transition = Transition.End;
                        
                    }
                    break;
                case Transition.End:
                    EndFrame--;
                    if(EndFrame <= 0)
                    {
                        isGuarding = false;
                    }
                    break;
                    
            } 
        }
    }
    class Attack
    {
        public bool isAttacking;
        public Animator animator;

        public float attackPoint;
        public float upfukitobasiPower;
        public float sidefukitobasiPower;
        public Vector3 fukitobasiVector;
        public float preventTime;

        Transform transform;
        AttackBace attackBace;
        public AttackBace attackBaceProperty
        {
            get { return attackBace; }
        }
        SetUseAttack[] Attacks;
        
        /// <summary>
        /// 使う攻撃を判定
        /// </summary>
        /// <param name="setUseAttacks"></param>
        /// <param name="animator"></param>
        /// <param name="transform"></param>
        public Attack(SetUseAttack[] setUseAttacks, Animator animator,Transform transform)
        {
            Attacks = new SetUseAttack[setUseAttacks.Length];
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
                attackPoint = attackBace.AttackPoint;
                upfukitobasiPower = attackBace.UpFukitobasiPower;
                sidefukitobasiPower = attackBace.SideFukitobsiPower;
                fukitobasiVector = attackBace.FukitobasiVector;
                preventTime = attackBace.PreventTime;

            }
            else
            {
                attackPoint = 0;
                upfukitobasiPower = 0;
                sidefukitobasiPower = 0;
                fukitobasiVector = Vector3.zero;
                preventTime = 0;
                isAttacking = false;
            }
        }
    }
    class Damage
    {
        float gravityScale;
        float airResistance;
        float damagePoint;
        float upfukitobiPower;
        float sidefukitobiPower;
        Vector3 fukitobiVector;
        float dist;
        float waitTime;
        Transform transform;
        Animator animator;
        public Damage(Animator animator, Transform transform)
        {
            this.animator = animator;
            this.transform = transform;
        }
        public void Start(float gravityScale, float airResistance, float UpFukitobiPower,float SideFukitobiPower, Vector3 FukitobiVector, float WaitTime)
        {
            this.gravityScale = gravityScale;
            this.airResistance = airResistance;
            upfukitobiPower = UpFukitobiPower;
            sidefukitobiPower = SideFukitobiPower;
            fukitobiVector = FukitobiVector;
            this.waitTime = WaitTime;

        }
        public void Calcurate_HitPoint(ref float HitPoint, float damagePoint)
        {
            this.damagePoint = damagePoint;
            HitPoint -= damagePoint;
        }
        /// <summary>
        /// Finished True
        /// </summary>
        /// <returns></returns>
        public bool UPFukitobi()
        {

            if (upfukitobiPower > 0)
            {
                upfukitobiPower -= gravityScale;
                dist = upfukitobiPower / 15;
                transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
                return false;
            }
            else
            {
                return true;
            }
                      
        }
        /// <summary>
        /// Finished True
        /// </summary>
        /// <returns></returns>
        public bool SIDEFukitobi()
        {
            if (sidefukitobiPower > 0)
            {
                
                sidefukitobiPower -= airResistance;
                dist = sidefukitobiPower / 15;
                transform.position += fukitobiVector * dist * Time.fixedDeltaTime;
                return false;
            }
            else
            {
                return true;
            }
            
        }
        /// <summary>
        /// Finished True
        /// </summary>
        /// <returns></returns>
        public bool Wait()
        {
            waitTime--;
            return (waitTime <= 0);
        }
    }
}

class Step
{
    public bool isStepping;
    float moveSpeed;
    float moveDistance;
    float nowDistance;
    Transform transform;
    Vector3 direction;
    float invincibleFrame;
    public float invincibleProperty
    {
        get { return invincibleFrame; }
    }
    float startFrame, stepFrame, endFrame;
    public enum Transition
    {
        Start, Stepping, End
    }
    Transition transition;
    public Transition transitionProperty
    {
        get { return transition; }
    }

    public Step(Transform transform)
    {
        this.transform = transform;
    }
    public void Start(float moveSpeed, float invincibleFrame, Vector3 direction, bool isStepping)
    {
        this.moveSpeed = moveSpeed;
        this.direction = direction;
        this.invincibleFrame = invincibleFrame;

        startFrame = 3f; stepFrame = 5f; endFrame = 5f;
        this.isStepping = isStepping;
        if (isStepping)
        {
            transition = Transition.Start;
        }
    }

    public void Stepping(bool is_in_front_of_wall)
    {
        switch (transition) {
            case Transition.Start:
                startFrame--;
                if(startFrame <= 0)
                {
                    transition = Transition.Stepping;
                }
                break;
            case Transition.Stepping:
                invincibleFrame--;
                stepFrame--;

                if (stepFrame <= 0)
                {

                    transition = Transition.End;

                }
                else
                {
                    if (!is_in_front_of_wall)
                    {
                        transform.position += direction * moveSpeed * Time.fixedDeltaTime;

                    }
                }
                break;
            case Transition.End:
                endFrame--;
                if(endFrame <= 0)
                {
                    isStepping = false;
                }
                break;
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