﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {
 
    public bool guarding;
    public GameObject guardner;
    public string keycode;

    public string[] Names_Use_Recovery;
    Behaviour[] behaviours;

    void Start()
    {

        Guard_Reset();

        Behavi_Reset();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            Guard_Mth(true);
        }
        if (Input.GetKeyUp(keycode))
        {

            Guard_Mth(false);
        }

    }
    void Guard_Reset()
    {
        guardner = Instantiate(guardner, this.transform.position, this.transform.rotation);
        guardner.SetActive(false);

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
    void Guard_Mth(bool guard_ON_OFF)
    {
        if (guard_ON_OFF == true)
        {

            guarding = true;
            Recovery(true);

            guardner.SetActive(true);
          
            guardner.transform.position = this.transform.position;
            guardner.transform.eulerAngles = this.transform.eulerAngles;

        }
        if (guard_ON_OFF == false)
        {
            guarding = false;
            Recovery(false);
            guardner.SetActive(false);
        }
    }
    void Recovery(bool recovery_ON_OFF)
    {
        if (recovery_ON_OFF == true)
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = false;
            }
        }
        if (recovery_ON_OFF == true)
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = true;
            }
        }
    }

}
