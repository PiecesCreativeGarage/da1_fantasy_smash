using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player_Info
{
    public GameObject player;
    public bool wint_losef = true;

    public GameObject Camera;

    public GameObject canvas;
    public Text label;
    public string Win_message;
    public string Lose_message;
    

    
}

public class GameOver : MonoBehaviour
{


    //
    public GameObject Gameset_canvas;
    Text GameSet_label;
    public string GamesetMessage;
    public float wait_Time;
    //

    int num_of_player;
    public bool gameset_ON;

    public Player_Info[] player_Info;

    void Start()
    {
        num_of_player = player_Info.Length;

        GameSet_label = Gameset_canvas.gameObject.GetComponentInChildren<Text>();

        for(int i = 0; i < num_of_player; i++)
        {
            player_Info[i].label = player_Info[i].canvas.gameObject.GetComponentInChildren<Text>();
        }

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
            StartCoroutine("Wait");
            
        }
        
    }
    void Gameset()
    {
        






        for (int i = 0; i < player_Info.Length; i++)
        {
           if(player_Info[i].wint_losef == true)
            {
                player_Info[i].label.text = player_Info[i].Win_message;
                player_Info[i].canvas.SetActive(true);
            }
            if (player_Info[i].wint_losef == false)
            {
                player_Info[i].label.text = player_Info[i].Lose_message;
                player_Info[i].canvas.SetActive(true);
            }
        }
        Time.timeScale = 0;
        Destroy(this.gameObject);

    }
    IEnumerator Wait()
    {
        
        GameSet_label.text = GamesetMessage;
        Gameset_canvas.SetActive(true);

        
        yield return new WaitForSeconds(wait_Time);
        Gameset_canvas.SetActive(false);
        Gameset();
        

    }
}


