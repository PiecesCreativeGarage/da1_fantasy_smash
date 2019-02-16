using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Move
{
    string animationName;
    float moveSpeed;
    float airResistance;

    Transform transform;
    PlayerData playerData;

    public Move(Transform transform, PlayerData playerData)
    {
        this.transform = transform;
        this.playerData = playerData;
    }
    public void Start(string animationName)
    {
        this.animationName = animationName;
    }
    public void Update(Vector3 input, bool isGrounded, bool[] isHit_against_theWall)
    {
        this.moveSpeed = playerData.moveData.moveSpeed;
        this.airResistance = playerData.baseData.airResistance;
        if (!isHit_against_theWall[0])
        {
            if (isGrounded)
            {
                if (input.magnitude != 0)
                {
                    playerData.baseData.animator.SetBool(animationName, true);
                    transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    playerData.baseData.animator.SetBool(animationName, false);
                }
            }
            else
            {
                if (input.magnitude != 0)
                {
                    transform.position += transform.forward * (moveSpeed - airResistance) * Time.fixedDeltaTime;
                    playerData.baseData.animator.SetBool(animationName, false);
                }
            }
        }
    }
}
