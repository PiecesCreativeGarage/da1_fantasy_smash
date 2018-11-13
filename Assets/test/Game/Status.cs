using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public float Hit_Point;
    public float Attack_Point;
    public float Knock_Point;
    public bool NoDamage;
    public bool Down;
    //自作の指定
    public Attack[] Attacks;

    public Guard Guard;


    void Start()
    {
        Debug.Log(this.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        Attack_Mth();
        Guard_Mth();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent != null)
        {
            Status other_status_C = other.GetComponentInParent<Status>();
            if (other_status_C != null)
            {
                HP_Cal(other_status_C.Attack_Point);
            }
        }


    }
    void HP_Cal(float otherATP)
    {
        if (NoDamage == false)
        {

            this.Hit_Point += -otherATP;
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
                break;
            }
            else
            {
                ATP_Cal(0f);
            }
        }
    }
    void ATP_Cal(float Attack_ATP)
    {
        this.Attack_Point = Attack_ATP;


    }
    void Guard_Mth()//　自作　有り
    {
        if (Guard != null)
        {
            if (Guard.guarding == true)
            {
                this.NoDamage = true;
            }
            if (Guard.guarding == false)
            {
                this.NoDamage = false;
            }
        }
    }

}
