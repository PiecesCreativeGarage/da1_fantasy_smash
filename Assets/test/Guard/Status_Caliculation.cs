using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Caliculation : MonoBehaviour {

    [SerializeField]
    Status status;
	
    private void OnTriggerEnter(Collider other)
    {
        if (status.Guard == false)
        {
            Debug.Log("aaa");
            status.HP -= other.GetComponentInParent<Status>().Attack_Point;
        }
    }
}
