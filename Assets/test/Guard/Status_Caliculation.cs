using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Caliculation : MonoBehaviour {

    [SerializeField]
    Status status;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("ground"))
        {

            if (status.Guard == false)
            {
                if (other.GetComponentInParent<Status>() != null)
                {
                    status.HP -= other.GetComponentInParent<Status>().Attack_Point;
                }

            }
        }
        if (other.gameObject.CompareTag("guardbreaker"))
        {
            status.Guard = false;
        }

        if(other.gameObject.CompareTag("Weapon")) {
            Anime anime = gameObject.GetComponent<Anime>();
            anime.Hit(other.gameObject);
        }
    }
}
