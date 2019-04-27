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
            transform.position += Vector3.up * velo * Time.fixedDeltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Damager damager = (Damager)GetComponent("Damager");
       
        if (other.CompareTag("Player"))
        {
            if(damager != null)
            {
                if(damager.playerInstanceID == other.gameObject.GetInstanceID())
                {
                    
                    return;
                }
            }


            GameObject.Destroy(this.gameObject);
        }
        
    }

}
