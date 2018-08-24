using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Prefab : MonoBehaviour {

	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = GetComponentInParent<Transform>().transform.position;
	}
 
}
