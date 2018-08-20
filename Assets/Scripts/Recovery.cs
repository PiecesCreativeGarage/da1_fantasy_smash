using UnityEngine;

public class Recovery : MonoBehaviour {

    [SerializeField]
    Move move;
    [SerializeField]
    Rotation rotation;
	

    void Recovery_Start()
    {

        move.enabled = false;
        rotation.enabled = false;
    }
    void Recovery_End()
    {
        Debug.Log("aaaa");
        move.enabled = true;
        rotation.enabled = true;
    }
}
