using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_ : MonoBehaviour {

    public GameObject canvas;
    public Camera cam;
    public RenderMode renderMode;
    

    public float HP;

	// Use this for initialization
	void Start () {
        canvas.gameObject.GetComponent<Canvas>().renderMode = renderMode;
        canvas.gameObject.GetComponent<Canvas>().worldCamera = cam;

    }
	
	// Update is called once per frame
	void Update () {
        canvas.gameObject.GetComponentInChildren<Text>().text = HP.ToString();
	}
}
