using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public bool is_use_Gravity;
    public bool is_Ground;
    public float Gravity_Scale;
   
    float ac;
    float dist;
    public Vector3 center_plus;
    public Vector3 halfExtents;
    public string[] Names_Use_Recovery;
    Behaviour[] behaviours;

    void Start()
    {
        Behavi_Reset();
    }


    private void FixedUpdate()
    {
        if (is_use_Gravity)
        {
            
            if (Physics.CheckBox(this.transform.position + center_plus, halfExtents, Quaternion.identity))
            {
                
                is_Ground = true;
            }
            else
            {
                is_Ground = false;
            }
            if (is_Ground == false)
            {
                ac = Gravity_Scale;
                dist += ac * Time.fixedDeltaTime;
                this.transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
                Recovery(true);
            }
            else
            {
                if (dist != 0)
                {
                    dist = 0;
                    Recovery(false);
                }
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
