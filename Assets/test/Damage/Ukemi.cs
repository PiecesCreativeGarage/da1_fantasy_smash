using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ukemi : MonoBehaviour {

    public bool CanUkemi;
    public bool isc;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.eulerAngles.x >= 50)
        {
            if (CanUkemi)
            {
                CanUkemi = false;
                StartCoroutine(Cor());
            }
            if (isc && Input.anyKeyDown)
            {
                isc = false;

                this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
            }
        }
	}
    IEnumerator Cor()
    {
       
        isc = true;
        yield return new WaitForSeconds(0.5f);
        if (isc)
        {
            isc = false;
            yield return new WaitForSeconds(2f);
            isc = true;
        }
    }
}
