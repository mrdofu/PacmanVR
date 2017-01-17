using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int WINNER_PACMAN = 0;     // if pacman eats all dots before time runs out
    public const int WINNER_GHOST = 1;      // if pacman loses all lives or time runs out
    
    public GameObject ghostManager;
    public GameObject pacmanObj;
    public GameObject pacSpawn;

    // event for game pausing/ playing
    public delegate void GameStateChangeAction(bool isPaused);
    public static event GameStateChangeAction OnGameStateChanged;

    private bool gamePaused;
    public bool GamePaused {
        get { return gamePaused; }
        set {
            gamePaused = value;
            if (OnGameStateChanged != null)
            {
                OnGameStateChanged(value);
            }
        }
    }

    // all children of GameManager gameobject are dots
    void Start()
    {
        GamePaused = true;
    }

    void Update()
    {

    }

    /**
     * Called when pacman loses a life. Respawns ghosts and pacman from their spawn points
     */
    public void RespawnAll()
    {
        ghostManager.GetComponent<GhostManager>().RespawnGhosts();
        pacmanObj.transform.position = pacSpawn.transform.position;
    }

    /**
     * To be called after all pacman lives are lost or after time runs out or all dots are consumed
     * @param winner 0 if pacman wins, 1 if ghosts win
     */
    public void GameOver(int winner)
    {
        // Determine winner based on parameter
        GameObject[] hudArray = GameObject.FindGameObjectsWithTag("HUD");
        foreach (var hudObj in hudArray)
        {
            Text gameStatusText = hudObj.transform.Find("gameStatusText").GetComponent<Text>();
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
}