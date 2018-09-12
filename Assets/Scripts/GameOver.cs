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


    //とりあえず　やりかたがわからないので　エラー　は変数での判定にする
    bool error = false;
    private void Start()
    {
        if(camera_script == null)
        {
            Debug.Log("<color=red>GameOver カメラのスクリプトが指定されてない</color>");
        }
        if (status == null)
        {
            Debug.Log("<color=red>GameOver ステータスのスクリプトが指定されてない</color>");
            error = true;
        }
        if (GameSet_label == null)
        {
            Debug.Log("<color=red>GameOver Text(表示用オブジェクト)が指定されてない</color>");
        }
 
    }
       
    
    private void Update()
    {
        if(error == false) {
            
                if (status.HP <= 0)
                {
                    camera_script.enabled = false;

                    GameSet_label.enabled = true;


                    GameSet_label.text = GameSet_text;
                }
            
        }
    }
   
    
}
