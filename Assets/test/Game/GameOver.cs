using UnityEngine;
[System.Serializable]
public class Player_Info
{

    public Status status;
    public GameObject Camera;

}

public class GameOver : MonoBehaviour {
  
    public Player_Info[] player_Info;
    
	void Start () {
   
	}


    void Update() {
        /*
        for (int i = 0; i < player_Info.Length; i++) {
            if (player_Info[i].status.Playing == false)
            {
                Debug.Log("aaa");
            }

        
        }
        */
        if (player_Info[0].status == null) Debug.Log("aaa");
        if (player_Info[1].status == null) Debug.Log("eee");
    }
}
