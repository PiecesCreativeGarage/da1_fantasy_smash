using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public GameObject camera;

    float x;
    float z;

    Vector3 angle;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        angle = transform.eulerAngles;

        if(Input.GetKey("up"))
        {
            this.transform.eulerAngles = new Vector3(angle.x, (45 * x) + camera.transform.localRotation.y, angle.z);
        }
        else if (Input.GetKey("down"))
        {
            this.transform.eulerAngles = new Vector3(angle.x, (180 * z - 45 * x) + camera.transform.localRotation.y, angle.z);
        }
        else if(Input.GetKey("right") || Input.GetKey("left"))
        {
            this.transform.eulerAngles = new Vector3(angle.x, (90 * x) + camera.transform.localRotation.y, angle.z);
        }
        
       
    }
}
