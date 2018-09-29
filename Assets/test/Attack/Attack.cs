using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    Animation anim;
    public AnimationClip anicli;
	void Start () {
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Anime_Start");
        }
	}
    IEnumerator Anime_Start()
    {
        anim.clip = anicli;
        anim.Play("attack");
        yield return null;
    }
}
