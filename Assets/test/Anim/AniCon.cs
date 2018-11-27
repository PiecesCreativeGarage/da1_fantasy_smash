using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniCon : MonoBehaviour {

    public string key_Run = "Run";
    public string key_Attack1 = "Attack1";
    public string keycode_Attack1 = "k";
    public string key_Guard = "Guard";
    public string keycode_Guard = "g";
    public Animator animator;
    Vector3 input;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if(input.magnitude != 0)
        {
            Debug.Log("AAA");
            animator.SetBool(key_Run, true);
        }
        else
        {
            animator.SetBool(key_Run, false);
        }
        if (Input.GetKeyDown(keycode_Attack1))
        {
            animator.SetTrigger(key_Attack1);

        }
        if (Input.GetKeyDown(keycode_Guard))
        {
            animator.SetBool(key_Guard, true);
        }
        else if(Input.GetKeyUp(keycode_Guard))
        {
            animator.SetBool(key_Guard, false);
        }
	}
}
