using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour {
	[SerializeField]
	Status status;

    private Animator animator;
    private const string moving = "Moving";
    private const string attack1 = "Attack1Trigger";

    float x;
    float z;

	void Start () {
        this.animator = GetComponent<Animator>();
	}
	
	
	void Update () {
		x = Input.GetAxisRaw ("Horizontal");
		z = Input.GetAxisRaw ("Vertical");

		if (!status.Guard) {
			if (x != 0 || z != 0) {  
				this.animator.SetBool (moving, true);
			} else {
				this.animator.SetBool (moving, false);
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				this.animator.SetTrigger (attack1);
			}
		} else {
			this.animator.SetBool (moving, false);
		}
    }
}
