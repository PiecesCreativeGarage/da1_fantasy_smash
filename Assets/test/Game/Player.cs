using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Animator animator;

    public float moveSpeed;
    public float jumpPower;
    public float gravityScale;

    public float hitPoint;

    public GameObject guardPrefab;
    public GameObject cam;
    Rotation rotation;
    Move move;
    Gravity gravity;
    Jump jump;
    Guard guard;
    Attack attack;
    Damage damage;

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
        idle, jumpping, guarding, attacking, damaged, Down,
    }


    public Status player_status;
    public bool isGrounded;

    private void Awake()
    {
        rotation = new Rotation();
        move = new Move();
        jump = new Jump();
        gravity = new Gravity();
        guard = new Guard();
        attack = new Attack();
        damage = new Damage();
    }
    private void Start()
    { 

        rotation.Start(this.cam);
        move.Start(moveSpeed, animator);
        jump.Start(jumpPower, gravityScale);
        gravity.Start(gravityScale, this.transform);
        guard.Start(30, "g", guardPrefab);
        attack.Start(AttacksInfo, animator, transform);

    }
    private void Update()
    {

        isGrounded = GetGrounded(transform.position + new Vector3(0, 2), 0.5f, -transform.up, 3f);
        GetStatus();
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
                Input_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                move.Update(transform, Input_dir);
                rotation.Update(this.transform, Input_dir);
                jump.Update();
                gravity.Update(true);
                guard.Update();
                attack.Update();
                break;
            case Status.jumpping:
                jump.Jumpping(transform);
                break;
            case Status.guarding:
                guard.Guarding(this.transform);
                if (jump.isjumpping == false)
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
            case Status.damaged:

                switch (damage._EnumDameged)
                {
                    case Damage._enumDameged.damaged:
                        damage.Calcurate_HitPoint(ref this.hitPoint);
                        damage._EnumDameged = Damage._enumDameged.fukitobi;
                        break;
                    case Damage._enumDameged.fukitobi:

                        damage.Wait();
                        damage.UPFukitobi();
                        damage.SIDEFukitobi();

                        if (damage.UPFukitobi() && damage.SIDEFukitobi() && damage.Wait())
                        {
                            damage._EnumDameged = Damage._enumDameged.end;
                        }

                        break;
                    case Damage._enumDameged.end:
                        GetStatus(Status.idle);
                        damage._EnumDameged = Damage._enumDameged.damaged;
                        break;

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
                Input_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                move.Update(transform, Input_dir);
                rotation.Update(this.transform, Input_dir);
                gravity.Update(false);
                break;
            case Status.jumpping:
                jump.Jumpping(this.transform);
                if (jump.isjumpping == false)
                {
                    GetStatus(Status.idle);
                }
                break;
            case Status.damaged:

                switch (damage._EnumDameged)
                {
                    case Damage._enumDameged.damaged:
                        damage.Calcurate_HitPoint(ref this.hitPoint);
                        damage._EnumDameged = Damage._enumDameged.fukitobi;
                        break;
                    case Damage._enumDameged.fukitobi:

                        damage.Wait();
                        damage.UPFukitobi();
                        damage.SIDEFukitobi();

                        if (damage.UPFukitobi() && damage.SIDEFukitobi() && damage.Wait())
                        {
                            damage._EnumDameged = Damage._enumDameged.end;
                        }

                        break;
                    case Damage._enumDameged.end:
                        GetStatus(Status.idle);
                        damage._EnumDameged = Damage._enumDameged.damaged;
                        break;

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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponentInParent<Player>() != null)
        {
            Player otherPlayer = other.GetComponentInParent<Player>();
            GetStatus(Status.damaged);
            damage.Start(otherPlayer.attack.attackPoint,
                         otherPlayer.attack.upfukitobasiPower, 
                         otherPlayer.attack.sidefukitobasiPower, 
                         otherPlayer.attack.fukitobasiVector, 
                         otherPlayer.attack.preventTime, 
                         animator, transform);
            
        }
        else if(other.GetComponent<Damager>() != null)
        {
            Damager damager = other.GetComponent<Damager>();
            GetStatus(Status.damaged);
            damage.Start(damager.DamagePoint, 
                         damager.UpFukitobasiPower,
                         damager.SideFukitobsiPower,
                         damager.FukitobasiVector,
                         damager.PreventTime, 
                         animator, transform);
        }
    }
    class Move
    {
        float moveSpeed;
        Animator animator;
        /// <summary>
        /// SpeedとAnimatorの初期化
        /// </summary>
        /// <param name="PmoveSpeed"></param>
        /// <param name="animator"></param>
        public void Start(float PmoveSpeed,Animator animator)
        {
            moveSpeed = PmoveSpeed;
            this.animator = animator;
        }
        /// <summary>
        /// Animation再生とPositionの移動
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="input"></param>
        public void Update(Transform transform, Vector3 input)
        {
            if (input.magnitude != 0)
            {
                animator.SetBool("Run", true);
                transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
    }

    class Rotation
    {
        GameObject cam;
        bool use_cam;
        /// <summary>
        /// 使うCameraを設定
        /// </summary>
        /// <param name="Pcam"></param>
        public void Start(GameObject Pcam)
        {
            cam = Pcam;
            if (cam != null)
            {
                use_cam = true;
            }
        }
        /// <summary>
        /// 受け取ったTransformを回転
        /// Camera使う　Cameraに向き合わせる　　
        /// 違う　Quaternion.LookRotationで
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="dir"></param>
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

    class Gravity
    {
        float gravityScale;
        float value;
        Transform transform;
        public void Start(float gravityScale, Transform transform)
        {
            this.gravityScale = gravityScale;
            this.transform = transform;
      
        }
        public void Update(bool isGounded)
        {
            if (isGounded)
            {
                value = 0;
            }
            else
            {
                value -= gravityScale * Time.fixedDeltaTime;
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

        /// <summary>
        /// ジャンプ力と重力の設定
        /// </summary>
        /// <param name="Pjumppow"></param>
        /// <param name="Pgravity"></param>
        public void Start(float Pjumppow, float Pgravity)
        {
            jumpPow = Pjumppow;
            gravityScale = Pgravity;
        }
        /// <summary>
        /// Key入力の判定
        /// </summary>
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
        /// <summary>
        /// Key入力の判定
        /// </summary>
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
        public Animator animator;

        public float attackPoint;
        public float upfukitobasiPower;
        public float sidefukitobasiPower;
        public Vector3 fukitobasiVector;
        public float preventTime;

        Transform transform;
        AttackBace attackBace;
        SetUseAttack[] Attacks;
        
        /// <summary>
        /// 使う攻撃を判定
        /// </summary>
        /// <param name="setUseAttacks"></param>
        /// <param name="animator"></param>
        /// <param name="transform"></param>
        public void Start(SetUseAttack[] setUseAttacks, Animator animator,Transform transform)
        {
            Attacks = new SetUseAttack[setUseAttacks.Length];
            this.transform = transform;
            this.animator = animator;
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
                    attackBace.FukitobasiVector += transform.forward;
                    attackBace.animator = this.animator;
                    attackBace.Start();

                    if (!(attackBace == null))
                    {
                        isAttacking = true;
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
        public enum _enumDameged　//攻撃を受けたときの状態の変化
        {
            damaged, fukitobi, end,
        }
        public _enumDameged _EnumDameged;

        float damagePoint;
        float upfukitobiPower;
        float sidefukitobiPower;
        Vector3 fukitobiVector;
        float dist;
        float waitTime;
        Transform transform;
        Animator animator;
        public void Start(float DamagePoint,float UpFukitobiPower,float SideFukitobiPower, Vector3 FukitobiVector, float WaitTime, Animator animator, Transform transform)
        {
            damagePoint = DamagePoint;
            upfukitobiPower = UpFukitobiPower;
            sidefukitobiPower = SideFukitobiPower;
            fukitobiVector = FukitobiVector;
            this.waitTime = WaitTime;
            this.animator = animator;
            this.transform = transform;
        }
        public void Calcurate_HitPoint(ref float HitPoint)
        {
            HitPoint -= damagePoint;
        }
        /// <summary>
        /// Finished True
        /// </summary>
        /// <returns></returns>
        public bool UPFukitobi()
        {

            if (transform.position.y > 0 || upfukitobiPower > 0)
            {
                upfukitobiPower -= 9.8f;
                dist = upfukitobiPower * Time.fixedDeltaTime;
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
                
                sidefukitobiPower -= 3;
                dist = sidefukitobiPower * Time.fixedDeltaTime;
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
            animator.SetBool("Run", false);
            return (waitTime <= 0);
        }
    }
}
