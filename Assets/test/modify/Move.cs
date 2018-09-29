using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {



    public float MoveSpeed;

    float x;
    float z;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

            if (x != 0 || z != 0)
            {
                this.transform.position += MoveSpeed * this.transform.forward * Time.deltaTime;
            }
  
    }
}
