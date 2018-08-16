using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestend : MonoBehaviour
{
    Collider collider;
    void Start()
    {

        collider = GameObject.Find("Sword").GetComponent<BoxCollider>();

    }


    void Testend()
    {

        collider.enabled = false;

    }

}