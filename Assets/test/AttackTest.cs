using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour {
    Collider collider;
    void Start()
    {

        collider = GameObject.Find("Sword").GetComponent<BoxCollider>();

    }


    void Test()
    {

        collider.enabled = true;

    }




}
