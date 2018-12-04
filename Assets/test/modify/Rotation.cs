using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {


    public GameObject cam;

    float x;
    float z;

    Vector3 angle_this;
    Vector3 angle_camera;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");


            angle_this = transform.eulerAngles;
        
            angle_camera = cam.transform.eulerAngles;


            if (Input.GetKey("up"))
            {
                this.transform.eulerAngles = new Vector3(angle_this.x, (45 * x) + angle_camera.y, angle_this.z);
            }
            else if (Input.GetKey("down"))
            {
            Debug.Log("down");
                this.transform.eulerAngles = new Vector3(angle_this.x, (180 * z - 45 * x) + angle_camera.y, angle_this.z);
            }
            else if (Input.GetKey("right") || Input.GetKey("left"))
            {
            Debug.Log("side");

                this.transform.eulerAngles = new Vector3(angle_this.x, (90 * x) + angle_camera.y, angle_this.z);
            }

    }
     
}
