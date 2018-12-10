using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour {

    public float Hit_Point;

    
    public float Attack_Point;
    public Vector3 Fukitobi_Vector;
    public float Fukitobi_Point;

    bool is_useGravity;

    bool is_Guarding;
    bool success_guard;

    float guard_recoT;
    public bool is_CanUkemi;
    public bool isGround;
    public bool NoDamage;

    public bool Down;

    //自作の指定


    public Attack[] Attacks;

    public Guard Guard;
    
    public KnockBack KnockBack;
  
    public Gravity Gravity;
    public Jump Jump;
    public Ukemi Ukemi;
    void Start()
    {
        Gravity = GetComponent<Gravity>();

        
        Debug.Log(Jump);
        Debug.Log(Gravity);
    }
    // Update is called once per frame
    void Update()
    {
 
            Guard_Mth();

            Attack_Mth();
        
        Gravity_Mth();
        Jump_Mth();
        CanUkemi();
      
    }
    private void OnTriggerEnter(Collider other)
    {
        Status other_status_C = other.GetComponentInParent<Status>();
        if (other.transform.parent != null)
        {

            if (other_status_C != null)
            {
                if (NoDamage == false)
                {
                    if (is_Guarding)
                    {
                        guard_recoT = other_status_C.Attack_Point * 5;
                        success_guard = true;
                    }
                    HP_Cal(other_status_C.Attack_Point);
                    this.KnockBack_Mth(other_status_C.Fukitobi_Vector, other_status_C.Fukitobi_Point);
                }
            }
           
            
        }
     
        

    }
    void HP_Cal(float otherATP)
    {
        if (NoDamage == false)
        {
            if (is_Guarding)
            {
                otherATP /= 1000;
            }
            
        
                this.Hit_Point -= otherATP;
            
        }
        if (this.Hit_Point <= 0)
        {

            Down = true;
        }
        else
        {
            Down = false;
        }

    }

    void Attack_Mth()　//自作　有り
    {

        for (int i = 0; i < Attacks.Length; i++)
        {
            if (Attacks[i].Existing == true)
            {
                ATP_Cal(Attacks[i].Attack_Point);
                FUP_Cal(Attacks[i].Fukitobi_Point);
                FuVe_Cal(Attacks[i].Fukitobi_Vector);
                
                break;
            }
            else
            {
                ATP_Cal(0f);
                FUP_Cal(0f);
                FuVe_Cal(Vector3.zero);
        
            }
        }
    }
    void ATP_Cal(float Attack_ATP)
    {
        this.Attack_Point = Attack_ATP;
    }
    void FUP_Cal(float FuP)
    {
        Fukitobi_Point = FuP;
    }
    void FuVe_Cal(Vector3 Attack_Vector)
    {
        this.Fukitobi_Vector = Attack_Vector;
    }


    void Guard_Mth()//　自作　有り
    {
        if (Guard != null)
        {
            if (Guard.guarding == true)
            {
                this.is_Guarding = true;
                if (success_guard)
                {
                    Guard.recovery_Time += guard_recoT;
                    success_guard = false;
                }
            }
            if (Guard.guarding == false)
            {
                this.is_Guarding = false;
            }
        }
    }
    void KnockBack_Mth(Vector3 other_Fukitobi_Vector, float other_fukitobi_point) //自作　有
    {
        if(KnockBack != null)
        {
            if (is_Guarding)
            {
               other_fukitobi_point *= 0.7f;
         
            }
            else
            {
                if(other_fukitobi_point >= 200)
                {
                    this.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
                }
            }
                KnockBack.Fukitobi_Vector = other_Fukitobi_Vector;
                KnockBack.fukitobi_point_value = other_fukitobi_point;
                KnockBack.is_Fukitobi = true;
           
        }
    }
    
    void Jump_Mth()
    {
        if(Jump != null)
        this.Jump.is_ground = isGround;
        else
            Jump = GetComponent<Jump>();
    }
    
    void Gravity_Mth()
    {
        if (Gravity != null)
        {
            this.isGround = Gravity.is_Ground;
        }
    }
    void CanUkemi()
    {
        if (Ukemi != null)
        {
            if (this.is_CanUkemi && isGround)
            {

                Ukemi.CanUkemi = true;
                KnockBack.is_CanUkemi = is_CanUkemi = false;
            }
            else
            {
                is_CanUkemi = KnockBack.is_CanUkemi;

            }
        }
    }
}
