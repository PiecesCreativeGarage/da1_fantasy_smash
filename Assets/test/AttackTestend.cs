using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestend : MonoBehaviour
{

    GameObject h;

    void Start()
    {
        h = GameObject.Find("Sword");
    }

    // Update is called once per frame
    void Testend() { 

            h.GetComponent<CapsuleCollider>().enabled = false;
        
    }
}