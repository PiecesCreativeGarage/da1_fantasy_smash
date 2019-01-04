using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour {

    float x, z;
    public bool isAble;
    public bool isMoving;
    public float distance;
    public float speed;
    public float distnow = 0;
    bool input_wait;
    int input_times;

    public string[] Names_Use_Recovery;
    Behaviour[] behaviours;


    void Start () {
        Behavi_Reset();

	}

    // Update is called once per frame
    void Update()
    {
        if (isAble)
        {
            if (isMoving)
            {

                if (x != 0)
                {
                    Move_Posi(this.transform.right, x);
                }
                else if (z != 0)
                {
                    Move_Posi(this.transform.forward, z);
                }

            }
            else
            {
                x = Input.GetAxisRaw("Horizontal");
                z = Input.GetAxisRaw("Vertical");

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (!input_wait)
                    {
                        StartCoroutine(Input_Wait());

                    }
                    input_times++;
                    if (input_times == 2)
                    {
                        input_times = 0;
                        isMoving = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (!input_wait)
                    {
                        StartCoroutine(Input_Wait());

                    }
                    input_times -= 3;
                    if (input_times == -6)
                    {
                        input_times = 0;
                        isMoving = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (!input_wait)
                    {
                        StartCoroutine(Input_Wait());

                    }
                    input_times += 2;
                    if (input_times == 4)
                    {
                        input_times = 0;
                        isMoving = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (!input_wait)
                    {
                        StartCoroutine(Input_Wait());

                    }
                    input_times -= 2;
                    if (input_times == -4)
                    {
                        input_times = 0;
                        isMoving = true;
                    }
                }

            }
        }
    }
    void Move_Posi(Vector3 forward, float XorZ)
    {
        this.transform.position += forward * XorZ * distance * speed * Time.fixedDeltaTime;
        distnow += distance * Time.fixedDeltaTime * speed;
        if (distnow >= distance)
        {
            isMoving = false;
            distnow = 0;
        }

    }
    IEnumerator Input_Wait()
    {
  
        input_wait = true;
        Debug.Log(input_wait);
        yield return new WaitForSeconds(3f);
        input_wait = false;
        Debug.Log(input_wait);
        input_times = 0;
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
