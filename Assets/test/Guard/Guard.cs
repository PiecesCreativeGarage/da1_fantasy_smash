    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    [SerializeField]
    Recovery recovery;

    [SerializeField]
    Status status;
    [SerializeField]
    GameObject guard;
	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.G))
        {
            //recovery.Anime_Recovery_Start();

            status.Guard = true;
            guard.SetActive(true);
        }
        else
        {
            //recovery.Anime_Recovery_End();

            guard.SetActive(false);
            status.Guard = false;
        }
	}
}
