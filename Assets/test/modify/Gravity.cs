using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Gravity
{
    public float gravityScale;
    float value;
    Transform transform;
    public Gravity(Transform transform)
    {
        this.transform = transform;
    }
    public void Update(bool isGounded, float gravityScale)
    {
        this.gravityScale = gravityScale;
        if (isGounded)
        {
            value = 0;        
        }
        else
        {
            value -= gravityScale / 15;
            transform.position += new Vector3(0, value * Time.fixedDeltaTime);
        }
    }
}