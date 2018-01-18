using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int WINNER_PACMAN = 0;     // if pacman eats all dots before time runs out
    public const int WINNER_GHOST = 1;      // if pacman loses all lives or time runs out
    
    // events for changing game states
    public static event Action OnGamePaused;
    public static event Action OnGamePlayed;
    public static event Action OnGameReset;     // soft reset (ie pacman dies)
    public static event Action OnGameRestart;   // hard game reset (ie after game over)

    void Start()
    {
        OnGamePaused();
    }

    void OnEnable()
    {
        PlayScaleTarget.OnPlayButtonComplete += PlayScaleTarget_OnPlayButtonComplete;
        Pacman.OnLoseLife += Pacman_OnLoseLife;
        Pacman.OnLoseAllLives += Pacman_OnLoseAllLives;
    }

    void OnDisable()
    {
        PlayScaleTarget.OnPlayButtonComplete -= PlayScaleTarget_OnPlayButtonComplete;
        Pacman.OnLoseLife -= Pacman_OnLoseLife;
        Pacman.OnLoseAllLives -= Pacman_OnLoseAllLives;
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

    /* EVENT CALLBACKS */

    private void PlayScaleTarget_OnPlayButtonComplete() {
        OnGamePlayed();
    }

    private void Pacman_OnLoseLife() {
        OnGameReset();
    }

    private void Pacman_OnLoseAllLives() {
        GameOver(WINNER_GHOST);
    }

    public void HomeMenu_OnPointerClick() {
        OnGameRestart();
    }

    /* END EVENT CALLBACKS */
}