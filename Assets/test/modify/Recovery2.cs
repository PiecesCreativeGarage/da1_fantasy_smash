using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery2 : MonoBehaviour {

    public Component component;
	void Start () {
        Recovery_Start(component);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Recovery_Start(Object obj)
    {
        Debug.Log(obj.GetType().ToString());
    }
    void Recovery_End(object obj)
    {

    }
}
