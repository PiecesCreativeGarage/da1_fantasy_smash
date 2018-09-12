using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public GameObject target;
    
   
    public Vector3 distance;
    public Vector3 LookPosi;
    private void Start()
    {
        if(target == null)
        {
            Debug.Log("<color=red>Camera_Script ターゲットが指定されてない</color>");
            this.enabled = false;
        }
    }
    private void LateUpdate()
    {
     
            this.transform.position = target.transform.position + distance;
            this.transform.LookAt(target.transform.position + LookPosi);
       
    }
}
