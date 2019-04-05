using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Damage
{
    PlayerData playerData;

    float gravityScale;
    float airResistance;
    float damagePoint;
    float upfukitobiPower;
    float sidefukitobiPower;
    Vector3 fukitobiVector;
    float dist;
    float waitTime;
    Transform transform;
    public Damage(Transform transform, PlayerData playerData)
    {
 
        this.transform = transform;
        this.playerData = playerData;
    }
    public void Start(float gravityScale, float airResistance, float UpFukitobiPower, float SideFukitobiPower, Vector3 FukitobiVector, float WaitTime)
    {
        this.gravityScale = gravityScale;
        this.airResistance = airResistance;
        upfukitobiPower = UpFukitobiPower;
        sidefukitobiPower = SideFukitobiPower;
        fukitobiVector = FukitobiVector;
        this.waitTime = WaitTime;

    }
    public void Calcurate_HitPoint(ref float HitPoint, float damagePoint)
    {
        this.damagePoint = damagePoint;
        HitPoint -= damagePoint;
    }
    /// <summary>
    /// Finished True
    /// </summary>
    /// <returns></returns>
    public bool UPFukitobi(bool[] ishit_against_theWall)
    {
        if (!(ishit_against_theWall[0] && ishit_against_theWall[1]))
        {
            if (upfukitobiPower > 0)
            {
                upfukitobiPower -= gravityScale;
                dist = upfukitobiPower;
                transform.position += new Vector3(0, dist * Time.fixedDeltaTime);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }
    /// <summary>
    /// Finished True
    /// </summary>
    /// <returns></returns>
    public bool SIDEFukitobi(bool[] ishit_against_theWall)
    {
        if (!(ishit_against_theWall[0] && ishit_against_theWall[1]))
        {
            if (sidefukitobiPower > 0)
            {
                sidefukitobiPower -= airResistance;
                dist = sidefukitobiPower;
                transform.position += fukitobiVector * dist * Time.fixedDeltaTime;
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Finished True
    /// </summary>
    /// <returns></returns>
    public bool Wait()
    {
        waitTime--;
        return (waitTime <= 0);
    }

}

