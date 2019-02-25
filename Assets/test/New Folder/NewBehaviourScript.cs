using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public float moveSpeed;
    public GameObject cam;
    public Vector3 beforePosition;
    Vector3 InputDir;
    bool isHit;
    
    Rotation rotation;
	void Start () {
        rotation = new Rotation(transform, cam);
        rotation.use_cam = true;
	}

    // Update is called once per frame
    void Update()
    {
        InputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        rotation.Update(InputDir);
        isHit = GetHit(beforePosition ,transform.position, transform.forward, 0.5f);
        if (isHit)
        {

        }
        else
        {
            if (InputDir.magnitude != 0)
            {
                beforePosition = transform.position;
                this.transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
            }
        }
    }
    
    bool GetHit(Vector3 beforePosition, Vector3 origin, Vector3 direction, float maxDistance)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit raycastHit;
        Vector3 offset = transform.position - beforePosition;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Debug.DrawRay(transform.position, offset.normalized * 100, Color.green);
        
        if(Physics.Raycast(ray, out raycastHit, maxDistance))
        {
            Debug.Log("111");
            transform.position += -ray.direction * raycastHit.distance * Time.fixedDeltaTime;
            return true;
        }
        else if(Physics.Raycast(transform.position, //offset.normalized
                beforePosition, out raycastHit, 100))
        {
            Debug.Log("aaa");
            transform.position = beforePosition * 3 * Time.fixedDeltaTime;

            return true;
        }
        else
        {
            return false;
        }
        
    }
}
