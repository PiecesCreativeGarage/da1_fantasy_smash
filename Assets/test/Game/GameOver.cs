using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    [System.Serializable]
    public class Player_Info
    {
        public Player player;
        public Text text;
        public Image image;
        public bool win_lose;　// Win True   Lose False
    };

    public Player_Info[] player_Info;
    public Text GameSet_Label;
    int remainingPlayers = 0; //残り人数
    float[] hp_maxs;
    float[] hp1s_fil1s;
    bool isGameSet = false;

    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameSet) { 
            for (int i = 0; i < player_Info.Length; i++)
            {
                if (remainingPlayers == 0)
                {
                    StartCoroutine(GameSet());
                }
                else
                {
                    ShowHP(i);
                    if (player_Info[i].player.player_status == Player.Status.Down)
                    {
                        player_Info[i].win_lose = false;
                        remainingPlayers--;
                    }
                }
            }
        }
        else
        {
            if (Input.anyKeyDown && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                ReroadScene();
            }
        }
       
    }
    void Initialization()
    {
        remainingPlayers = player_Info.Length;
        hp_maxs = new float[remainingPlayers];
        hp1s_fil1s = new float[remainingPlayers];

        for (int i = 0; i < player_Info.Length; i++)
        {

            hp_maxs[i] = player_Info[i].player.hitPoint;
            hp1s_fil1s[i] = 1 / hp_maxs[i];
            player_Info[i].win_lose = true;

            player_Info[i].text.text = "";
        }
        GameSet_Label.text = "";

    }
    IEnumerator GameSet()
    {
        Time.timeScale = 0;
        isGameSet = true;
        Time.timeScale = 1;
        GameSet_Label.text = "GameSet";
        yield return new WaitForSeconds(1);
        GameSet_Label.text = "";

        for (int i = 0; i < player_Info.Length; i++)
        {
            
            if (player_Info[i].win_lose)
            {
                DealWIN_or_LOSE(true, i);
            }
            else
            {
                DealWIN_or_LOSE(false, i);
            }
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < player_Info.Length; i++)
        {
            player_Info[i].text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        GameSet_Label.text = "Input anykey..";
        Time.timeScale = 0;
    }
    void DealWIN_or_LOSE(bool victory, int i)
    {
        player_Info[i].image.enabled = false;
       
        if (victory)
        {
            player_Info[i].text.text = "WIN";
        }
        else
        {
            player_Info[i].text.text = "LOSE";
        }
    }
    /// <summary>
    /// player_Info[引数].hitPoint と
    /// fillAmountを表示
    /// </summary>
    /// <param name="number"></param>
    void ShowHP(int number)
    {
         player_Info[number].text.text = player_Info[number].player.hitPoint.ToString();
         player_Info[number].image.fillAmount = 
            1 - (hp1s_fil1s[number] * (hp_maxs[number] - player_Info[number].player.hitPoint));
    }
    void ReroadScene()
    {

        isGameSet = false;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}


