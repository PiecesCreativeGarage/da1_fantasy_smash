using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTTT : MonoBehaviour {
    [System.Serializable]
    public struct AAA
    {
        public Vector3[] vector3s;
    }
    public AAA[] aaa;
    public int a;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            aaa[1].vector3s = new Vector3[aaa[0].vector3s.Length + 6];
            for(a = 0; a < aaa[0].vector3s.Length; a++)
            {
                aaa[1].vector3s[a] = aaa[0].vector3s[a];
            }
        }
	}
}
