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
        public Image image;
        public bool win_lose;
    };

    public Player_Info[] player_Info;
    public Text GameSet_Label;
    int num;　//人数　カウンタ

    int nn = 0; //人数 かうんた
   

    float[] hp_maxs;
    float[] hp1s_fil1s;
    bool GameSet = false;
    bool is_deel_GameSet = false;
   
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_deel_GameSet == false)
        {
            if (nn == 1)
            {
                is_deel_GameSet = true;
                StartCoroutine("Game_Over");
            }
            else
            {
                for (num = 0; num < player_Info.Length; num++)
                {

                    if (player_Info[num].status.Down == true)
                    {
                        player_Info[num].win_lose = false;
                        player_Info[num].image.fillAmount = 0;
                        nn--;

                    }
                    HP_show(num);
                }
            }
        }
        if (GameSet == true && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            ReroadScene();
        }
    }
    void Initialization()
    {
        nn = player_Info.Length;
        hp_maxs = new float[nn];
        hp1s_fil1s = new float[hp_maxs.Length];
        for (num = 0; num < player_Info.Length; num++)
        {

            hp_maxs[num] = player_Info[num].status.Hit_Point;
            hp1s_fil1s[num] = 1 / hp_maxs[num];
            player_Info[num].win_lose = true;
            player_Info[num].text.text = "";
        }
        GameSet_Label.text = "";

    }
    void HP_show(int num)
    {

        if (nn > 1)
        {

            player_Info[num].text.text = player_Info[num].status.Hit_Point.ToString();
            player_Info[num].image.fillAmount = 1 - (hp1s_fil1s[num] * (hp_maxs[num] - player_Info[num].status.Hit_Point));
        }

    }
    

    IEnumerator Game_Over()
    {
        Time.timeScale = 0;
        GameSet_Label.text = "Game Set";
        yield return new WaitForSecondsRealtime(1);
        GameSet_Label.text = "";
        for (int i = 0; i < player_Info.Length; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Win_Lose(i);
        }
        yield return new WaitForSecondsRealtime(2f);
        for(int i = 0; i < player_Info.Length; i++)
        {
            player_Info[i].text.enabled = false;
        }
        GameSet_Label.fontSize = 30;
        GameSet_Label.text = "続行するには何かキーを押してください...";
        GameSet = true;
    }
    void Win_Lose(int i)
    {
        player_Info[i].image.enabled = false;
        player_Info[i].text.rectTransform.localPosition = new Vector3(0, 0, 0);
        player_Info[i].text.fontSize = 20;
        player_Info[i].player.SetActive(false);
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

