﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {
    public Vector3 Fukitobi_Vector;
    public float side_fukitobi_point_value;
    float side_fukitobi_point;
   
    public float up_fukitobi_point_value;
    float up_fukitobi_point;
    public float dist;

    Vector3 up_fukitobi_pos;

    public float gravity;
    public float air_regist;

    public bool is_Fukitobi;
   
    bool is_up_Fukitobi;
    bool is_side_Fukitobi;
    bool is_can_ukemi;
    

    public const float GROUND_POSITION = 0;
    public const float FUKITOBI_MINIMUM_POWER = 0;

    public string[] Names_Use_Recovery;
    Behaviour[] behaviours;
    // Use this for initialization
    private void Start()
    {
        Behavi_Reset();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (is_Fukitobi)
        {
            Damaged();
            this.Fukitobi();
        }
        if(is_can_ukemi && Input.anyKeyDown)
        {
            this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            Recovery(false);
            is_can_ukemi = false;
        }
    
    }
    void Damaged()
    {

        if (is_up_Fukitobi == false)
        {
            up_fukitobi_point = up_fukitobi_point_value;
            is_up_Fukitobi = true;
            Recovery(true);
            if (up_fukitobi_point > 200)
            {
                this.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }


        if (is_side_Fukitobi == false && side_fukitobi_point_value > FUKITOBI_MINIMUM_POWER)
        {
            side_fukitobi_point = side_fukitobi_point_value;
            is_side_Fukitobi = true;
            Recovery(true);
            if (side_fukitobi_point > 10)
            {
                this.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }
    }
    private void Fukitobi()
    {
        
        if (is_up_Fukitobi)
        {
            up_fukitobi_point += gravity;
            dist += up_fukitobi_point * Time.fixedDeltaTime;
            up_fukitobi_pos = new Vector3(this.transform.position.x, dist * Time.fixedDeltaTime, this.transform.position.z);
            this.transform.position = up_fukitobi_pos;//0にいるため

            if (up_fukitobi_point < FUKITOBI_MINIMUM_POWER)
            {
                if (Physics.CheckBox(this.transform.position + new Vector3(0, -1f, 0), new Vector3(0.5f, 0.25f, 0.5f))) { 
                    dist = 0;
                up_fukitobi_point_value = 0;
                is_up_Fukitobi = false;
               }
            }
        }
        
        if (is_side_Fukitobi)
        {
            side_fukitobi_point += air_regist;
            this.transform.position += Fukitobi_Vector * side_fukitobi_point * Time.fixedDeltaTime;
        }
        if(side_fukitobi_point < FUKITOBI_MINIMUM_POWER)
        {
            side_fukitobi_point_value = 0;
            is_side_Fukitobi = false;
        }

    
        if(is_up_Fukitobi == false && is_side_Fukitobi == false)
        {
            is_Fukitobi = false;
            StartCoroutine(Can_Ukemi(0.5f, 3f));
        }

    }
    void Behavi_Reset()
    {
        behaviours = new Behaviour[Names_Use_Recovery.Length];
        for (int i = 0; i < Names_Use_Recovery.Length; i++)
        {
            behaviours[i] = (Behaviour)GetComponent(Names_Use_Recovery[i]);
            if (behaviours[i] == null)
            {
                Debug.Log(Names_Use_Recovery[i]);
            }
        }
    }
    void Recovery(bool recovery_ON_OFF)
    {
        Debug.Log(recovery_ON_OFF);
        if (recovery_ON_OFF == true)
        {

            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = false;
            }
        }

        if (recovery_ON_OFF == false)
        {

            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = true;
            }
        }

    }
    IEnumerator Can_Ukemi(float can_ukemi_time, float down_time)
    {
        is_can_ukemi = true;
        yield return new WaitForSeconds(can_ukemi_time);
        if (is_can_ukemi)
        {
            is_can_ukemi = false;
            yield return new WaitForSeconds(down_time);
            is_can_ukemi = true;
        }
    }
    
}