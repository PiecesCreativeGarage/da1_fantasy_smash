using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTeatSword : MonoBehaviour {

	
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
    }

}
