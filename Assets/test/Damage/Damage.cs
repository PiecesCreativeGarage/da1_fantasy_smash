using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    public bool Damaged_ON_OFF;
    public bool Knocked_ON_OFF;
    public float Knock_Point;

    public float Bdbck_Seconds;

        public string[] Names_Use_Recovery;
        Behaviour[] behaviours;

        void Start()
        {

            Behavi_Reset();
        }


        // Update is called once per frame
        void Update()
        {
        if(Damaged_ON_OFF == true)
        {
            StartCoroutine(Damaged());
        }
        if(Knocked_ON_OFF == true)
        {

            this.transform.position = -this.transform.forward * Knock_Point * Time.deltaTime;
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
    void Recovery(bool recovery_ON_OFF)
    {
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
    IEnumerator Damaged()
    {
        Damaged_ON_OFF = false;
        Knocked_ON_OFF = true;
        Recovery(true);
        yield return new WaitForSeconds(Bdbck_Seconds);
        Recovery(false);
        Knocked_ON_OFF = false;
    }
  
}
