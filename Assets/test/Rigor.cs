using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigor : MonoBehaviour {

    public GameObject gameobject;

	void Start () {

	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameobject.GetComponent<Move>().enabled = false;
        }
	}
}
