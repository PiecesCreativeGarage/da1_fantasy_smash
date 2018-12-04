using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public string keycode;
    public float jumppower_value;
    float ac;
    float dist;
    public float gravity;


    public bool is_ground;
    public bool is_jumpping;
    

    private void Start()
    {
        
    }
    private void Update()
    {
        if(Physics.CheckBox(this.transform.position + new Vector3(0, -1f, 0), new Vector3(0.5f, 0.25f, 0.5f)))
        {
            is_ground = true;
        }
        else
        {
            is_ground = false;
        }

        if (is_ground)
        {
            if (Input.GetKeyDown(keycode))
            {
                is_jumpping = true;
            }
        }

        if (is_jumpping)
        {
            ac = jumppower_value;
            
            is_ground = false;
            is_jumpping = false;
        }
        if(is_ground == false)
        {
            ac += gravity;
            dist -= ac * Time.deltaTime;
            this.transform.position += new Vector3(0, dist * Time.deltaTime, 0);
        }
    }
}
