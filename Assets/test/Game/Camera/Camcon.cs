using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcon : MonoBehaviour {

    public float rotate_Speed;
	public float camFollowSpeed = 2;
	float camInvalidDistance = 1;

    Vector3 angle;

    public GameObject cam;
    public Vector3 cam_posi;
    public GameObject player;
    public Vector3 look_posi;

    public bool targetCam_ON_OFF;
    public GameObject target;

    void Start()
    {

        cam.transform.parent = this.transform;
        Cam_Posi();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (player != null && cam != null)
        {
            This_Posi();

        }
        if (targetCam_ON_OFF)
        {
            Target_Camera();
        }

    }
    void This_Posi()
    {
#if true // 前のカメラ処理に戻したかったら true を false にしてね
		// ターゲットとプレイヤーの距離を計算
		float distance = Vector3.Distance(target.transform.position, player.transform.position);
		if (distance < camInvalidDistance) {
			// 距離が一定以内だったらカメラ座標位置を更新しない
			return;
		}
		// プレイヤーの位置まで線形補間させながらカメラ移動させる
		//   Lerpとは：第一引数と第二引数の間の、第三引数(0.0～1.0)で指定した割合の値を返すメソッド。 
		//             例) 0.0なら第一引数の座標、0.5は双方の半分の座標、1.0なら第二引数の座標
		this.transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * camFollowSpeed);
#else
		transform.position = player.transform.position;
#endif
	}

    void Cam_Posi()
    {
        cam.transform.position = this.transform.localPosition + cam_posi;

    }
    void Target_Camera()
    {
        //maincamera.transform.LookAt(target.transform.position);
        this.transform.LookAt(target.transform.position);
    }
}