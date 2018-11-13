using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcon : MonoBehaviour {

    public float rotate_Speed;
    Vector3 angle;

    public GameObject cam;
    public Vector3 cam_posi;
    public GameObject player;
    public Vector3 look_posi;

    public bool targetCam_ON_OFF;
    public GameObject target;
    void Start()
    {

        cam.transform.parent = this.transform;
        Cam_Posi();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (player != null && cam != null)
        {
            This_Posi();

        }
        if (targetCam_ON_OFF)
        {
            Target_Camera();
        }

    }
    void This_Posi()
    {
        this.transform.position = player.transform.position;
    }

    void Cam_Posi()
    {
        cam.transform.position = this.transform.localPosition + cam_posi;

    }
    void Target_Camera()
    {
        //maincamera.transform.LookAt(target.transform.position);
        this.transform.LookAt(target.transform.position);
    }
}