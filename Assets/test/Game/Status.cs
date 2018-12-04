using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour {

    public float Hit_Point;

    
    public float Attack_Point;
    public Vector3 Fukitobi_Vector;
    public float Up_Fukitobi_Point;
    public float Side_Fukitobi_Point;


    public bool is_Guarding;

    float guard_recoT;

    public bool NoDamage;

    public bool Down;

    //自作の指定


    public Attack[] Attacks;

    public Guard Guard;
    
    public KnockBack KnockBack;

    void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
 
            Guard_Mth();

            Attack_Mth();
      
    }
    private void OnTriggerEnter(Collider other)
    {
        Status other_status_C = other.GetComponentInParent<Status>();
        if (other.transform.parent != null)
        {

            if (other_status_C != null)
            {
                if (is_Guarding)
                {

                }
                else
                {
                    HP_Cal(other_status_C.Attack_Point);
                    this.KnockBack_Mth(other_status_C.Fukitobi_Vector, other_status_C.Up_Fukitobi_Point, other_status_C.Side_Fukitobi_Point);
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
                FUP_Cal(Attacks[i].Up_Fukitobi_Power, Attacks[i].Side_Fukitobi_Power);
                FuVe_Cal(Attacks[i].Fukitobi_Vector);
                
                break;
            }
            else
            {
                ATP_Cal(0f);
                FUP_Cal(0f, 0f);
                FuVe_Cal(Vector3.zero);
        
            }
        }
    }
    void ATP_Cal(float Attack_ATP)
    {
        this.Attack_Point = Attack_ATP;
    }
    void FUP_Cal(float KnockBack_uFUP, float KnockBack_sFUP)
    {
        this.Up_Fukitobi_Point = KnockBack_uFUP;
        this.Side_Fukitobi_Point = KnockBack_sFUP;
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
             
            }
            if (Guard.guarding == false)
            {
                this.is_Guarding = false;
            }
        }
    }
    void KnockBack_Mth(Vector3 other_Fukitobi_Vector, float other_Up_Fukitobi_Point, float other_Side_Fukitobi_Point) //自作　有
    {
        if(KnockBack != null)
        {
            if (NoDamage == false)
            {
                
                    KnockBack.Fukitobi_Vector = other_Fukitobi_Vector;
                    KnockBack.up_fukitobi_point_value = other_Up_Fukitobi_Point;
                    KnockBack.side_fukitobi_point_value = other_Side_Fukitobi_Point;
                    KnockBack.is_Fukitobi = true;
                
            }
        }
    }


}
