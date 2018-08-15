using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene ("BgStage1", LoadSceneMode.Additive); 

		Object prefab = Resources.Load ("Character/2HandedWarrior");
		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.localEulerAngles = new Vector3 (0, 180, 0);
		go.transform.position = new Vector3 (10, 0, 10);
	}
}
