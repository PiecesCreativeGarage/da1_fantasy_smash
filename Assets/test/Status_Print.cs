using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Print : MonoBehaviour {
    [SerializeField]
    Status status;

    [SerializeField]
    UnityEngine.UI.Text HP_label;

	
	void Update () {

        HP_label.text = status.HP.ToString();
        if(status.HP <= 0)
        {
            HP_label.enabled = false;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        status.HP -= other.GetComponentInParent<Status>().Attack_Point;
    }
}
