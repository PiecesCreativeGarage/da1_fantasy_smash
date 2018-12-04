using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour {

    public Vector3 plus;
    public Vector3 ans;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ans = this.transform.forward + plus;
        this.transform.position += ans * 3 * Time.fixedDeltaTime;
	}
}
