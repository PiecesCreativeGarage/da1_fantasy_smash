using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    [SerializeField]
    bool jump;

    [SerializeField]
    float a;
    [SerializeField]
    float p;
    [SerializeField]
    float q;

    float x; //時間
    
    Vector3 startPosition;


    void Start () {
        jump = false;
	}
	
	// Update is called once per frame
	void Update () {


        if (jump == false) {

            if (Input.GetKeyDown(KeyCode.J))
            {
                jump = true;
                startPosition = transform.position;
            }

        }


        if(jump == true)
        {   
            //ジャンプ　ちょっとずつ　上にいく        (x - p) が　＋の値になったら下がる
            transform.position += new Vector3(0, -a * (x - p) + q * Time.deltaTime);
            x++;

            if(transform.position.y <= startPosition.y)
            {
                jump = false;
                x = 0;
            }
        }
	}
}
