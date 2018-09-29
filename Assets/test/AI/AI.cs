using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float MoveSpeed;
    public int turnTime;
    int i = 1;
    int RL = 1;

    public int a;
    public GameObject target;
    public float distance;

    
	void Start () {


   

	}

    void Update()
    {
        switch(a){
            case 1:
            if (i == turnTime * 60)
            {
                RL *= -1;
                i = 1;
            }

            this.transform.position += (transform.right * RL) * MoveSpeed * Time.deltaTime;
            i++;
                break;
            case 2:

                if (Mathf.Abs(transform.position.x) - Mathf.Abs(target.transform.position.x) > distance || Mathf.Abs(transform.position.x) - Mathf.Abs(target.transform.position.x) < -distance)
                {
                    this.transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                }
                else if (Mathf.Abs(transform.position.z) - Mathf.Abs(target.transform.position.z) > distance || Mathf.Abs(transform.position.z) - Mathf.Abs(target.transform.position.z) < -distance)
                {
                    this.transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                }
                else
                {
                    
                    Attaking.attaking = true;
                   
                }




                this.transform.LookAt(target.transform.position);
                break;
        }
    }
}
