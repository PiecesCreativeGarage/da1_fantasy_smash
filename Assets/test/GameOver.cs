using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    [SerializeField]
    Camera_Script camera_script;
    [SerializeField]
    Status status;
    [SerializeField]
    UnityEngine.UI.Text GameSet_label;

    [SerializeField]
    string GameSet_text;

    private void Update()
    {
        if (status.HP <= 0)
        {   
            camera_script.enabled = false;

            GameSet_label.enabled = true;


            GameSet_label.text = GameSet_text;
        }
    }
   
    
}
