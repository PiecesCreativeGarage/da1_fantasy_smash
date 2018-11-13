using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [System.Serializable]
    public class Player_Info
    {
        public GameObject player;
        public Status status;
        public Camera cam;
        public Text text;
        public bool win_lose;
    };

    public Player_Info[] player_Info;
    int num;
    int nn = 0;
    bool GameSet = false;
   
    void Start()
    {
        Instance();
    }

    // Update is called once per frame
    void Update()
    {

        if (nn == 1)
        {
            Game_Over();
        }
        else
        {
            for (num = 0; num < player_Info.Length; num++)
            {
                HP_show(num);
                if (player_Info[num].status.Down == true)
                {
                    player_Info[num].win_lose = false;
                    nn--;
                }
            }
        }
        if (GameSet == true && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);
            ReroadScene();
        }
    }
    void Instance()
    {
        nn = player_Info.Length;

        for (num = 0; num < player_Info.Length; num++)
        {
            player_Info[num].win_lose = true;
            player_Info[num].text.text = "";
        }

    }
    void HP_show(int num)
    {

        player_Info[num].text.text = player_Info[num].status.Hit_Point.ToString();
    }
    void Game_Over()
    {
        for (int i = 0; i < player_Info.Length; i++)
        {
            Win_Lose(i);
        }

        Time.timeScale = 0;
        GameSet = true;
    }
    void Win_Lose(int i)
    {

        if (player_Info[i].win_lose == true)
        {
            player_Info[i].text.text = "Win";
        }
        if (player_Info[i].win_lose == false)
        {
            player_Info[i].text.text = "Lose";
        }

    }

    void ReroadScene()
    {

        GameSet = false;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}

