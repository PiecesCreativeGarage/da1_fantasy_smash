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

    void Start()
    {
        ac = 0;
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

            }
            else
            {
                dist = 0;
            }
        }
    }
}
