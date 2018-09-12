using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Print2 : MonoBehaviour {

    [SerializeField]
    Status status;

    [SerializeField]
    UnityEngine.UI.Text HP_label;

    bool error;
    
    private void Start()
    {
        error = DebugUtil.ErrorNotice(this.ToString(), "status", status);
        error |= DebugUtil.ErrorNotice(this.ToString(), "HP_label", HP_label);
    }
    


    void Update()
    {
        if (error != true)
        {
            HP_label.text = status.HP.ToString();
            if (status.HP <= 0)
            {
                HP_label.enabled = false;
            }
        }
    }

}
