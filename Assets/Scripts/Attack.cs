using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    [SerializeField]
    Collider coll;
 


    void Attack_Start()
    {

        coll.enabled = true;

    }
    void Attack_End()
    {

        coll.enabled = false;

    }


}
