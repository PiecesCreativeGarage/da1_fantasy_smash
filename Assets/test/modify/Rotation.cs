using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Rotation
{
    Transform transform;
    GameObject cam;
    public bool use_cam;

    public Rotation(Transform transform, GameObject Pcam)
    {
        this.transform = transform;
        cam = Pcam;
        if (cam != null)
        {
            use_cam = true;
        }
    }


    public void Update(Vector3 dir)
    {
        if (use_cam)
        {
            if (dir.z == 1)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, (dir.x * 45) + cam.transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else if (dir.z == -1)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, (180 * dir.z) - (dir.x * 45) + cam.transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else if (dir.x != 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, (dir.x * 90) + cam.transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }

    }
}
