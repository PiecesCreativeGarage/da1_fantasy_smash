using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {
    public float hp;
    float hp_max;
    float val = 0;
    public Image bar;
	// Use this for initialization
	void Start () {
        hp_max = hp;
        val = bar.fillAmount / hp_max;
    }
	
	// Update is called once per frame
	void Update () {
HP_cal();


	}
    void HP_cal()
    {
        Debug.Log("HPcal");
        bar.fillAmount = 1 - (val * (hp_max - hp));
    }
}
