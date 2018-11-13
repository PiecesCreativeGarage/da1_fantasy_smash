using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU_test : MonoBehaviour {

   
    public string key_Attack1 = "Attack1";
    public string keycode_Attack1;
    public Animator animator;
    Vector3 input;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(keycode_Attack1))
        {
            animator.SetTrigger(key_Attack1);

        }
    }
}
