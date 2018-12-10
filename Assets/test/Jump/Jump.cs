using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public string keycode;
    public float jumppower_value;
 
    float jumppower;
    public float gravity;
    public float dist;

    public bool is_ground;
    public bool is_jumpping;
  
    public string[] Names_Use_Recovery;
    Behaviour[] behaviours;
    private void Start()
    {
        Behavi_Reset();
    }
    private void Update()
    {
        if (is_ground)
        {
            if (Input.GetKeyDown(keycode))
            {
                Recovery(true);
                dist = 0;
                jumppower = jumppower_value;
                is_ground = false;
                is_jumpping = true;
            }
        }
        if (is_jumpping)
        {
            jumppower += gravity;
            dist = jumppower * Time.fixedDeltaTime;
            this.transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
            if(jumppower <= 0)
            {
                Recovery(false);
                is_jumpping = false;

               
            }
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



}
