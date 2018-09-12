using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		string bg_name = "BgStage1";
		// ステージを読み込む
		AsyncOperation sync = SceneManager.LoadSceneAsync (bg_name, LoadSceneMode.Additive);
		while (!sync.isDone) {
			// 読み込み待ち
			yield return null;
		}

		// 2Pを生成
		Create2p ();
	}

	void Create2p() {
		// 黄色い人を読みこむ
		Object prefab = Resources.Load ("Character/2HandedWarrior");
		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.localEulerAngles = new Vector3 (0, 180, 0);
		go.transform.position = new Vector3 (10, 0, 10);

        /*
		// 2P用カメラを生成
		Object camera_prefab = Resources.Load ("PlayerCamera");
		GameObject go_cam = (GameObject)Instantiate (camera_prefab);
		Camera cam = go_cam.GetComponent<Camera> ();
		Camera_Script cam_sc = go_cam.GetComponent<Camera_Script> ();
		cam_sc.target = go;

		Camera.main.rect = new Rect(0f, 0.5f, 1f, 0.5f);
		cam.rect = new Rect (0f, 0f, 1f, 0.5f);*/
	}
}
