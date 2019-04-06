using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Gravity
{
    float value;
    float velo;
    Transform transform;
    PlayerData playerData;
    public Gravity(Transform transform, PlayerData playerData)
    {
        this.transform = transform;
        this.playerData = playerData;
    }
    public void Update(bool isGounded)
    {
        if (isGounded)
        {
            value = 0;        
        }
        else
        {
            Debug.Log(isGounded);
            value -= playerData.baseData.gravityScale;
            velo = value * Time.fixedDeltaTime;
            transform.position += new Vector3(0, value) * Time.fixedDeltaTime;
        }
    }
}