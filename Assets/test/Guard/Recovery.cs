using UnityEngine;

public class Recovery : MonoBehaviour {
    [SerializeField]
    Anime anime;
    [SerializeField]
    Move move;
    [SerializeField]
    Rotation rotation;


    public void Recovery_Start()
    {

        move.enabled = false;
        rotation.enabled = false;
    }
    public void Recovery_End()
    {
        
        move.enabled = true;
        rotation.enabled = true;
    }


    public void Anime_Recovery_Start()
    {
        anime.enabled = false;
    }
    public void Anime_Recovery_End()
    {
        anime.enabled = true;
    }
}
