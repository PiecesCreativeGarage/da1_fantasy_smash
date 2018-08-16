﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestAnime : MonoBehaviour {

    private Animator animator;

    private const string test = "Test";
	void Start () {
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.animator.SetBool(test, true);
        }
        else
        {
            this.animator.SetBool(test, false);
        }
	}
}
