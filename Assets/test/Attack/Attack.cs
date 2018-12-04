using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public string Keycode;

    public bool Attakking;
    public bool Existing;

    public float Attack_Point;
    public Vector3 Fukitobi_Vector_plus;
    public Vector3 Fukitobi_Vector;
    public float Side_Fukitobi_Power;
    public float Up_Fukitobi_Power;

    public float Unguard_Seconds_B;
    public float Existing_Seconds;
    public float Unguard_Seconds_A;

    public Collider Coll;
    public GameObject Gobj;

    public string[] Names_Use_Recovery;
    Behaviour[] behaviours;

    void Start()
    {
        Behavi_Reset();
    }

    void Update()
    {
        if (Input.GetKeyDown(Keycode) && Attakking == false)
        {
            StartCoroutine("Attack_Mth");
        }
        Fukitobi_Vector = this.transform.forward + Fukitobi_Vector_plus;
    }


    void Behavi_Reset()
    {
        behaviours = new Behaviour[Names_Use_Recovery.Length];
        for (int i = 0; i < Names_Use_Recovery.Length; i++)
        {
            behaviours[i] = (Behaviour)GetComponent(Names_Use_Recovery[i]);
            if (behaviours[i] == null)
            {
                Debug.Log(Names_Use_Recovery[i]);
            }
        }
    }


    IEnumerator Attack_Mth()
    {
        this.Attakking = true;
        this.Recovery(true);
        yield return new WaitForSeconds(Unguard_Seconds_B);
        
        if (Coll != null)
        {
            Coll.enabled = true;
            this.Existing = true;
        }
        if (Gobj != null)
        {
            Instantiate(Gobj, this.transform.position, this.transform.rotation);
        }
     
        yield return new WaitForSeconds(Existing_Seconds);

        if (Coll != null)
        {
            Coll.enabled = false;
            this.Existing = false;
        }
      
        yield return new WaitForSeconds(Unguard_Seconds_A);
        this.Recovery(false);
        this.Attakking = false;
     


    }

    void Recovery(bool recovery_ON_OFF)
    {
        Debug.Log(recovery_ON_OFF);
        if (recovery_ON_OFF == true)
        {

            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = false;
            }
        }

        if (recovery_ON_OFF == false)
        {

            for (int i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].enabled = true;
            }
        }

    }
}
