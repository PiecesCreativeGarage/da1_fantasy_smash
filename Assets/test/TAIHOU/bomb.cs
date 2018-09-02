using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public float LifeTime;

    public float gravity;
    public float mass;
    public float pow;

    float f1g;
    float Fg;


    float Fs;

    public GameObject GameObject;

    public Vector3 coefficient;
    

    public Vector3 posi;


    public float Ac;

    public float Velo;


    // Use this for initialization
    private void Start()
    {
        posi = transform.position;

        Destroy(this.gameObject, LifeTime);

        coefficient = GameObject.transform.forward;

        f1g = gravity * mass;

        Fg = pow * coefficient.x;

        Fs = pow;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Fg -= f1g;

        Ac = Fg / mass;
        Velo = Ac * Time.fixedDeltaTime;
        posi += new Vector3(0, Velo * Time.fixedDeltaTime);

        Ac = Fs / mass;
        Velo = Ac * Time.fixedDeltaTime;
        posi += coefficient * Velo * Time.fixedDeltaTime;


        transform.position = posi;

    
    }
}
