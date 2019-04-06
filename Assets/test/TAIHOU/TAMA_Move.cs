using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAMA_Move : MonoBehaviour {

    public float movePow_hori;
    public float movePow_ver;
    public bool useGravity;
    public float gravityScale;
    float dist;
    float velo;
    public float lifeTime;

	void Start () {
        GameObject.Destroy(this.gameObject, lifeTime);
	}
	

	void Update () {
        transform.position += transform.forward * movePow_hori * Time.fixedDeltaTime;

        if (useGravity)
        {
            movePow_ver += gravityScale;
            velo += movePow_ver * Time.fixedDeltaTime;
            dist += velo * Time.fixedDeltaTime;
            transform.position += Vector3.up * velo;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Ground")
        {
            GameObject.Destroy(this.gameObject);
        }
        
    }

}
