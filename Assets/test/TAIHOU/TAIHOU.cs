using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAIHOU : MonoBehaviour {

    [SerializeField]
    GameObject TAMA;
    [SerializeField]
    bool Fire = false;

	void Start () {
		
	}
	
	
	void Update () {
		if(Fire == true)
        {
            Instantiate(TAMA, this.transform.position, this.transform.rotation);
            Fire = false;
        }
	}

  
}
