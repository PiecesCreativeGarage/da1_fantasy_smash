using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAMA_Move : MonoBehaviour {

    public float moveSpeed;
    public float time;

	void Start () {
        GameObject.Destroy(this.gameObject, time);
	}
	

	void Update () {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;      
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Ground")
        {
            GameObject.Destroy(this.gameObject);
        }
        
    }

}
