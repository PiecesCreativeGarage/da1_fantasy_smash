using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoroller : MonoBehaviour {

    public GameObject player;
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + new Vector3(0, 3, 8);
        TargetCameraMethod();
	}
    void TargetCameraMethod()
    {
        transform.LookAt(target.transform.position);
    }
}
