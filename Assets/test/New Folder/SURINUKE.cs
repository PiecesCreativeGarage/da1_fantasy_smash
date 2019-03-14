using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SURINUKE : MonoBehaviour {

    public float moveAmount;
    Vector3 direction;
    float toSurfaceLength = 0.6f;   // 中心座標から物体表面までの長さ

    Vector3 beforePosition;

    private void Update()
    {

        beforePosition = this.transform.position;

        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            this.transform.position = this.transform.position + (direction * moveAmount);
        }

        Surinuke();
    }

    void Surinuke()
    {
        Vector3 diff =  transform.position - beforePosition;
        Vector3 foward = diff.normalized;
        Vector3 backward = -foward;        // 移動先に対して正反対の方向
        Vector3 surfaceOffset = foward * toSurfaceLength;

        RaycastHit raycastHit;
        // 前方にゆっくり進もうとした時用に物体にめり込まないように前方に対して当たり判定する
        if (Physics.Raycast(transform.position, foward, out raycastHit, toSurfaceLength))
        {
            // ヒットした場所と物体表面が接するように座標を正す
            transform.position = raycastHit.point - surfaceOffset;
        }

        float move_amount = diff.sqrMagnitude;      // 移動量
        // 物体表面から移動反対方向に移動量分の線分を飛ばし、線分に対して物体がヒットしていたら、高速ですり抜けたということになる
        if (Physics.Raycast(transform.position, backward, out raycastHit, move_amount))
        {
            // ヒット方向と進行方向のベクトルの内積を算出して、１以上なら同じ方向にいる物体なので前方に進もうとした時に衝突したということ
            bool is_foward_obj = 0 < Vector3.Dot(raycastHit.point -　beforePosition, foward);
            if (is_foward_obj)
            {
                // ヒットした場所と物体表面が接するように座標を正す
                transform.position = raycastHit.point - surfaceOffset;
            }
        }
    }
}
