using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public float moveSpeed;
    float turnTime;
    int dire = 1;

	void Update () {
		if(turnTime < 0)
        {
            dire *= -1;
            SetTime(60f);
        }
        else
        {
            turnTime--;
            this.transform.position += (transform.right * dire) * moveSpeed * Time.fixedDeltaTime;
        }

	}
    void SetTime(float Time)
    {
        turnTime = Time;
    }
}
