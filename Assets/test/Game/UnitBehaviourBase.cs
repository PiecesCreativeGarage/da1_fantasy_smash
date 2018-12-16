using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviourBase : MonoBehaviour {

    protected Behaviour[] behaviours;

    protected void Recovery(bool recovery_ON_OFF)
    {
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
