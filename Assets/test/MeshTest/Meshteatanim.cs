using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshteatanim : MonoBehaviour {

    const string attack = "Attack1Trigger";
    public bool attacking;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update () {
		if(attacking == true)
        {
            this.animator.SetTrigger(attack);
        }
	}
}
