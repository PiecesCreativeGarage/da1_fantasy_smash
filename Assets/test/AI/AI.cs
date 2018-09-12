using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float MoveSpeed;
    public int turnTime;
    int i = 1;
    int RL = 1;
	void Start () {

   

	}

    void Update()
	{
        if (i == turnTime * 60)
        {
            RL *= -1;
            i = 1;
        }

        this.transform.position += (transform.right * RL) * MoveSpeed * Time.deltaTime;
     i++;
	}
}
