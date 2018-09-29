using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

	public int HitPoint;
    public int AttackPoint;
    public int GuardPoint;

    public bool Playing;

	void Start () {
		
	}
	
	
	void Update () {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        HP_Cal(other);
        
    }

    void HP_Cal(Collider other_HP)
    {
        if (other_HP.gameObject.CompareTag("Weapon"))
        {
            if (other_HP.GetComponentInParent<Status>() != null)
            {
                HitPoint -= other_HP.GetComponentInParent<Status>().AttackPoint;

                if (HitPoint <= 0)
                {
                    Playing = false;
                    Destroy(this.gameObject);

                }
            }

        }
    }

}
