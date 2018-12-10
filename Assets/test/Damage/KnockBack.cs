using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {
    public Vector3 Fukitobi_Vector;
    public float fukitobi_point_value;
    public float fukitobi_point;
    public float dist;

    public float air_regist;

    public bool is_Fukitobi;
    public bool is_CanUkemi;
    bool is_Init = true;

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
            Init();
            this.Fukitobi();
        }
      
    
    }
    void Init()
    {
      
        if (is_Init)
        {
            Recovery(true);
            fukitobi_point = fukitobi_point_value;
            
            is_Init = false;
        }
    }
    private void Fukitobi()
    {
       
        if (fukitobi_point <= 0)
        {
            is_Init = true;
            dist = 0;
            is_Fukitobi = false;
            is_CanUkemi = true;
            Recovery(false);
        }
        fukitobi_point += air_regist;
        dist = fukitobi_point * Time.deltaTime;
        this.transform.position += Fukitobi_Vector * dist * Time.deltaTime;
        
    
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
    
    
}
