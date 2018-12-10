using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {
 
    public bool guarding;
    public float recovery_Time;
    float default_recoT;
    public GameObject guardner;
    public string keycode;
    public Vector3 guard_Posi;
    public Vector3 guard_Scale;

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
        if (guarding)
        {
            guardner.transform.localScale = guard_Scale;
            guardner.transform.position = this.transform.position + guard_Posi;
            guardner.transform.eulerAngles = this.transform.eulerAngles;
            if (recovery_Time != 0)
            {
                recovery_Time--;
                
            }
        }
        if (!Input.GetKey(keycode) && recovery_Time <= 0)
        {

            Guard_Mth(false);
        }

    }
    void Guard_Reset()
    {
        default_recoT = recovery_Time;
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
            

        }
        if (guard_ON_OFF == false)
        {
            guarding = false;
            Recovery(false);
            guardner.SetActive(false);
            recovery_Time = default_recoT;
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
        if (recovery_ON_OFF == false)
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = true;
            }
        }
    }

}
