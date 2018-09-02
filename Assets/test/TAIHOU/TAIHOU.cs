using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAIHOU : MonoBehaviour {

    [SerializeField]
    GameObject TAMA;
    [SerializeField]
    bool Fire = false;

	void Start () {
        StartCoroutine(FirePerSeconds());

	}

    IEnumerator FirePerSeconds()
    {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            Fire = true;
        }
    }
	
	void Update () {
		if(Fire == true)
        {
            GameObject go = (GameObject)Instantiate(TAMA, this.transform.position, Quaternion.identity);
            bomb bom = go.GetComponent<bomb>();
            bom.GameObject = this.gameObject;
            Fire = false;
        }
	}

  
}
