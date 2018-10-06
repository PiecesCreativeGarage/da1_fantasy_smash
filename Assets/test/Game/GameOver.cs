using UnityEngine;
using System.Collections;
[System.Serializable]
public class Player_Info
{
    public GameObject player;
    public bool wint_losef = true;

    public GameObject Camera;
    
    public UnityEngine.UI.Text label;
    public string Win_message;
    public string Lose_message;
    

    
}

public class GameOver : MonoBehaviour
{
    public GameObject canvas;

    //
    public UnityEngine.UI.Text GameSet_label;
    public string GamesetMessage;
    public float wait_Time;
    //

    int num_of_player;
    public bool gameset_ON;

    public Player_Info[] player_Info;

    void Start()
    {
        num_of_player = player_Info.Length;
    }


    void Update()
    {
        for (int i = 0; i < player_Info.Length; i++)
        {
            if (player_Info[i].player == null)
            {
                num_of_player--;
                player_Info[i].wint_losef = false;

            }
            if(num_of_player <= 1)
            {
                gameset_ON = true;
                break;
            }


        }
        if(gameset_ON == true)
        {
            //
            GameSet_label.enabled = true;
            GameSet_label.text = GamesetMessage;
            StartCoroutine("Wait");
            

            //
            Gameset();
            Time.timeScale = 0;
            
        }
        
    }
    void Gameset()
    {
        

        for (int i = 0; i < player_Info.Length; i++)
        {
           if(player_Info[i].wint_losef == true)
            {
                player_Info[i].label.text = player_Info[i].Win_message;
            }
            if (player_Info[i].wint_losef == false)
            {
                player_Info[i].label.text = player_Info[i].Lose_message;
            }
        }

        canvas.SetActive(true);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait_Time);
    }
}


