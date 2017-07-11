using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int WINNER_PACMAN = 0;     // if pacman eats all dots before time runs out
    public const int WINNER_GHOST = 1;      // if pacman loses all lives or time runs out
    
    public GameObject ghostManager;
    public GameObject pacmanObj;
    public GameObject pacSpawn;

    // events for changing game states
    public static event Action OnGamePaused;
    public static event Action OnGamePlayed;
    public static event Action OnGameReset;

    // all children of GameManager gameobject are dots
    void Start()
    {
        OnGamePaused();
    }

    void Update()
    {

    }

    void OnEnable()
    {
        PlayScaleTarget.OnPlayButtonComplete += PlayScaleTarget_OnPlayButtonComplete;
    }

    void OnDisable()
    {
        PlayScaleTarget.OnPlayButtonComplete -= PlayScaleTarget_OnPlayButtonComplete;
    }

    private void PlayScaleTarget_OnPlayButtonComplete()
    {
        OnGamePlayed();
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