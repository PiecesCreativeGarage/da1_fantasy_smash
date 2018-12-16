using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : UnitBehaviourBase {
    public Vector3 Fukitobi_Vector;
    public float side_fukitobi_point_value;
    float side_fukitobi_point;

    public float up_fukitobi_point_value;
    float up_fukitobi_point;
    public float dist;


    public float gravity;
    public float air_regist;

    public bool is_Fukitobi;

    bool is_up_Fukitobi;
    bool is_side_Fukitobi;
    bool is_can_ukemi;
    bool is_fuk;

    public const float GROUND_POSITION = 0;
    public const float FUKITOBI_MINIMUM_POWER = 0;

    public string[] Names_Use_Recovery;

    // Use this for initialization
    private void Start()
    {
        Behavi_Reset();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (is_Fukitobi)
        {
            Damaged();
            this.Fukitobi();
        }
        if (is_can_ukemi && Input.anyKeyDown)
        {
            this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            Recovery(false);
            is_can_ukemi = false;
        }

    }
    void Damaged()
    {

        if (is_up_Fukitobi == false)
        {
            up_fukitobi_point = up_fukitobi_point_value;
            is_up_Fukitobi = true;
            Recovery(true);
            if (up_fukitobi_point_value > 50)
            {
                this.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }


        if (is_side_Fukitobi == false && side_fukitobi_point_value > FUKITOBI_MINIMUM_POWER)
        {
            side_fukitobi_point = side_fukitobi_point_value;
            is_side_Fukitobi = true;
            Recovery(true);
            if (side_fukitobi_point > 20)
            {
                this.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }



    }
    private void Fukitobi()
    {

        if (is_up_Fukitobi)
        {


            if (up_fukitobi_point < FUKITOBI_MINIMUM_POWER)
            {
                dist += gravity * Time.deltaTime;
                this.transform.position += new Vector3(0, dist * Time.deltaTime);
                if (Physics.CheckBox(this.transform.position + new Vector3(0, -1f, 0), new Vector3(0.5f, 0.25f, 0.5f)))
                {
                    dist = 0;
                    up_fukitobi_point_value = 0;
                    is_up_Fukitobi = false;
                }
            }
            else
            {
                up_fukitobi_point += gravity;
                dist += up_fukitobi_point * Time.fixedDeltaTime;
                this.transform.position += new Vector3(0, dist * Time.fixedDeltaTime);

            }
        }

        if (is_side_Fukitobi)
        {
            side_fukitobi_point += air_regist;
            this.transform.position += Fukitobi_Vector * side_fukitobi_point * Time.fixedDeltaTime * Time.deltaTime;
        }
        if (side_fukitobi_point < FUKITOBI_MINIMUM_POWER)
        {
            side_fukitobi_point_value = 0;
            is_side_Fukitobi = false;
        }


        if (is_up_Fukitobi == false && is_side_Fukitobi == false)
        {
            is_Fukitobi = false;
            StartCoroutine(Can_Ukemi(0.5f, 3f));
        }

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

    IEnumerator Can_Ukemi(float can_ukemi_time, float down_time)
    {
        is_can_ukemi = true;
        yield return new WaitForSeconds(can_ukemi_time);
        if (is_can_ukemi)
        {
            is_can_ukemi = false;
            yield return new WaitForSeconds(down_time);
            is_can_ukemi = true;
        }
    }

}
