using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int WINNER_PACMAN = 0;
    public const int WINNER_GHOST = 1;

    public GameObject hud;

    // all children of GameManager gameobject are dots
    void Start()
    {
    }

    void Update()
    {

    }

    /**
     * To be called aft
     */
    public void GameOver(int winner)
    {
        // Determine winner based on parameter
        Text gameStatusText = hud.transform.Find("gameStatusText").GetComponent<Text>();
        if (winner == 0)
        {
            gameStatusText.text = "PACMAN WINS";
        }
        else
        {
            gameStatusText.text = "GHOSTS WIN";
        }
    }
}