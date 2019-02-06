using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Move
{
    string animationName;
    float moveSpeed;
    float airResistance;
    Animator animator;
    Transform transform;

    public Move(Transform transform, Animator animator)
    {
        this.transform = transform;
        this.animator = animator;
    }
    public void Start(float moveSpeed, float airResistance, string animationName)
    {
        this.moveSpeed = moveSpeed;
        this.airResistance = airResistance;
        this.animationName = animationName;
    }
    public void Update(Vector3 input, bool isGrounded, bool[] isHit_against_theWall)
    {
        if (!isHit_against_theWall[0])
        {
            if (isGrounded)
            {
                if (input.magnitude != 0)
                {
                    animator.SetBool(animationName, true);
                    transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    animator.SetBool(animationName, false);
                }
            }
            else
            {
                if (input.magnitude != 0)
                {
                    transform.position += transform.forward * (moveSpeed - airResistance) * Time.fixedDeltaTime;
                    animator.SetBool(animationName, false);
                }
            }
        }
    }
}
