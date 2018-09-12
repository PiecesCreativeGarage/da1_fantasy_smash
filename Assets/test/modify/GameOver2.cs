using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOver2 : MonoBehaviour {
    [SerializeField]
    Camera_Script camera_script;

    [SerializeField]
    Status status;

    [SerializeField]
    UnityEngine.UI.Text GameSet_label;

    [SerializeField]
    string GameSet_text;

    bool error;
    
    private void Start()
    {

        error = DebugUtil.ErrorNotice(this.ToString(), "camera_script", camera_script);
        error |= DebugUtil.ErrorNotice(this.ToString(), "status", status) == error;
        error |= DebugUtil.ErrorNotice(this.ToString(), "GameSet_label", GameSet_label) == error;

    }



    private void Update()
    {
        //
        if (error != true)
        {

            if (status.HP <= 0)
            {
                camera_script.enabled = false;

                GameSet_label.enabled = true;


                GameSet_label.text = GameSet_text;
            }

        }
    }
    //

}
 