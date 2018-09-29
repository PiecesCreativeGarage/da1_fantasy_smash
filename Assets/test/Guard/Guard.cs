﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    Animation anim;
    public AnimationClip anicli;
	void Start () {
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.G))
        {
            StartCoroutine("Anime_Start");
        }
        
        
	}
    IEnumerator Anime_Start()
    {
        anim.clip = anicli;
        anim.Play("Guard");
        yield return null;
    }
    
}
