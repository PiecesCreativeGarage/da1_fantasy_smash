using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {

    [SerializeField]
    float move;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = move * this.transform.forward * Time.deltaTime;
	}
}
