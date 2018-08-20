using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour {

    public int HP_Max;
    int HP;
    int HP_compare;

   
	void Start () {
        HP = HP_Max;
	}
	
	
	void Update () {
        if (HP != HP_compare)
        { 
            Debug.Log(HP);

            HP_compare = HP;
        }
	}
    private void OnTriggerEnter(Collider other)
    {

        HP -= other.GetComponentInParent<Attack_Power>().Attack_Point;
    }
}
