using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Print : MonoBehaviour {
    [SerializeField]
    Status status;

    [SerializeField]
    UnityEngine.UI.Text HP_label;

    //
    bool error;
    //
    private void Start()
    {
        if(status == null)
        {
            Debug.Log("<color=red>Status_Print ステータスのスクリプトが指定されてない</color>");
            error = true;
        }
        if (HP_label == null)
        {
            Debug.Log("<color=red>Status_Print Text(HP表示用オブジェクト)が指定されてない</color>");
            error = true;
        }
    }
    //


    void Update()
    {
        if (error == false)
        {
            HP_label.text = status.HP.ToString();
            if (status.HP <= 0)
            {
                HP_label.enabled = false;
            }
        }
    }
    
}
