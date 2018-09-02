using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour {
	[SerializeField]
	Status status;

    private Animator animator;
    private const string moving = "Moving";
    private const string attack1 = "Attack1Trigger";
    private const string guardbreak = "GuardBreakTrigger";

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
            if (Input.GetKeyDown(KeyCode.B))
            {
                this.animator.SetTrigger(guardbreak);
            }
		} else {
			this.animator.SetBool (moving, false);
		}

    }

    public void Hit(GameObject go)
    {
        // 当たった物体へのベクトルを得る
        Vector3 diff = transform.position - go.transform.position;
        Rigidbody rb = GetComponent<Rigidbody>();
        float pow = 10000;
        rb.AddForce(new Vector3(diff.x*pow, pow, diff.z*pow), ForceMode.Force);
    }
}
