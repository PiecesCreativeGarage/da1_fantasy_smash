using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Jump
{
    float jumpPow;
    float gravityScale;
    float velocity;
    float dist;
    public bool isjumpping;

    public void Start(float gravityScale, float jumpPower, bool isJumpping)
    {
        jumpPow = jumpPower;
        this.gravityScale = gravityScale;
        velocity = jumpPow;
        isjumpping = isJumpping;
    }
    public void Jumpping(Transform transform)
    {
        velocity -= gravityScale;
        dist = velocity / 15;
        transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
        if (velocity < 0)
        {

            dist = 0;
            velocity = 0;
            isjumpping = false;

        }
    }
}