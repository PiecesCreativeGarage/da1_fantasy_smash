using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2 : MonoBehaviour {


    [SerializeField]
    Recovery recovery;

    [SerializeField]
    Status status;
    [SerializeField]
    GameObject guard_Object;

    bool error;

    private void Start()
    {
        error = DebugUtil.ErrorNotice(this.ToString(), "recovery", recovery);
        error |= DebugUtil.ErrorNotice(this.ToString(), "status", status) == error;
        error |= DebugUtil.ErrorNotice(this.ToString(), "guardObject", guard_Object) == error;

    }
    // Update is called once per frame
    void Update()
    {
        if (error != true)
        {
            if (Input.GetKey(KeyCode.G))
            {
                //recovery.Anime_Recovery_Start();

                status.Guard = true;
                guard_Object.SetActive(true);
            }
            else
            {
                //recovery.Anime_Recovery_End();

                guard_Object.SetActive(false);
                status.Guard = false;
            }
        }
    }
}
