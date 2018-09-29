using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour {

	
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        Destroy(other.gameObject);
    }

}
