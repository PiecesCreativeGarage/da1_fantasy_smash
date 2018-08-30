using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public float gravity;
    public float mass;
    public float pow;

    float f1g;
    float Fg;


    float Fs;



    public Vector3 posi;


    float Ac;

    float Velo;

    // Use this for initialization
    private void Start()
    {
        f1g = gravity * mass;
        Fg = pow;

        Fs = pow;
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        Fg -= f1g;

        Ac = Fg / mass;
        Velo = Ac * Time.fixedDeltaTime;
        posi += transform.up * Velo * Time.fixedDeltaTime;

        Ac = Fs / mass;
        Velo = Ac * Time.fixedDeltaTime;
        posi += transform.forward * Velo * Time.fixedDeltaTime;


        transform.position = posi;

    
    }
}
