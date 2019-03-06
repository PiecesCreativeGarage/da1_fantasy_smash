using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SURINUKE : MonoBehaviour {

    public float moveAmount;
    Vector3 direction;

    public Vector3 beforePosition;

    private void Update()
    {
        
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            beforePosition = this.transform.position;
            this.transform.position = this.transform.position + (direction * moveAmount);
        }

        Surinuke();
    }

    void Surinuke()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, 1))
        {
            Debug.Log("!!!");
            transform.position += -transform.forward * moveAmount * Time.fixedDeltaTime;
        }
        else if(Physics.Raycast(transform.position, beforePosition, out raycastHit))
                //transform.position.magnitude - beforePosition.magnitude))
        {
            Debug.Log(beforePosition.normalized * 1);
            transform.position = raycastHit.point + (beforePosition.normalized * 1);
        }
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, beforePosition, Color.green);
    }
}
