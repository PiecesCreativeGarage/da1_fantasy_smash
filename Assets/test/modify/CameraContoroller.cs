using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoroller : MonoBehaviour {

    public GameObject player;
    public GameObject target;
    Vector3 diff;
	// Update is called once per frame
	void Update () {
        diff = target.transform.position - player.transform.position;
        transform.position =
            player.transform.position
            + new Vector3(-this.transform.forward.x * 8, 1.5f, -transform.forward.z * 8);
           
        TargetCameraMethod();
	}
    void TargetCameraMethod()
    {
        transform.LookAt(target.transform.position);
    }
}
