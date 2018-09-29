using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaking : MonoBehaviour {

    public static bool attaking;
    Animator animator;

    const string attackTrigger = "Attack1Trigger";
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(attaking == true)
        {
            Debug.Log("aaa");
            animator.SetTrigger(attackTrigger);
            attaking = false;
        }
    }

}
