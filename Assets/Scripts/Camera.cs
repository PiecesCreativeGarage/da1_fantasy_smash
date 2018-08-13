using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    public Vector3 LookPosi;
    public Vector3 distance;

    private void Update()
    {
        this.transform.position = target.transform.position - distance;
        this.transform.LookAt(target.transform.position + LookPosi);
    }
}
