using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public const int WINNER_PACMAN = 0;     // if pacman eats all dots before time runs out
    public const int WINNER_GHOST = 1;      // if pacman loses all lives or time runs out
    
    // events for changing game states
    public static event Action OnGamePaused;
    public static event Action OnGamePlayed;
    public static event Action OnGameReset;     // soft reset (ie pacman dies)
    public static event Action OnGameRestart;   // hard game reset (ie after game over)

    public enum GameState {
        Pause, Play
    };
    public static GameState CurrentState { get; private set; }

    const int MATCH_LENGTH = 3 * 60; // 3 minutes, in seconds
    private GameTimer matchTimer;

    void Start() {
        OnGamePaused();
        matchTimer = new GameTimer(MATCH_LENGTH);
    }

    void OnEnable() {
        PlayScaleTarget.OnPlayButtonComplete += PlayScaleTarget_OnPlayButtonComplete;
        Pacman.OnLoseLife += Pacman_OnLoseLife;
        Pacman.OnLoseAllLives += Pacman_OnLoseAllLives;
        Dot.OnAllDotsEaten += Dot_OnAllDotsEaten;
        OnGameRestart += GameManager_OnGameRestart;
        OnGamePaused += GameManager_OnGamePaused;
        OnGamePlayed += GameManager_OnGamePlayed;
        matchTimer.OnTimerComplete += MatchTimer_OnTimerComplete;
    }

    void OnDisable() {
        PlayScaleTarget.OnPlayButtonComplete -= PlayScaleTarget_OnPlayButtonComplete;
        Pacman.OnLoseLife -= Pacman_OnLoseLife;
        Pacman.OnLoseAllLives -= Pacman_OnLoseAllLives;
        Dot.OnAllDotsEaten -= Dot_OnAllDotsEaten;
        OnGameRestart -= GameManager_OnGameRestart;
        OnGamePaused -= GameManager_OnGamePaused;
        OnGamePlayed -= GameManager_OnGamePlayed;
        matchTimer.OnTimerComplete -= MatchTimer_OnTimerComplete;
    }

    /**
     * To be called after all pacman lives are lost or after time runs out or all dots are consumed
     * @param winner 0 if pacman wins, 1 if ghosts win
     */
    void GameOver(int winner) {
        // Determine winner based on parameter
        GameObject[] hudArray = GameObject.FindGameObjectsWithTag("HUD");
        foreach (var hudObj in hudArray) {
            Text gameStatusText = hudObj.transform.Find("gameStatusText").GetComponent<Text>();
            if (winner == 0) {
                gameStatusText.text = "PACMAN WINS";
            } else {
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

    private void Dot_OnAllDotsEaten() {
        GameOver(WINNER_PACMAN);
    }

    private void GameManager_OnGameRestart() {
        OnGamePaused();
        matchTimer.ResetTimer();
    }

    private void GameManager_OnGamePaused() {
        CurrentState = GameState.Pause;
    }

    private void GameManager_OnGamePlayed() {
        CurrentState = GameState.Play;
    }

    private void MatchTimer_OnTimerComplete() {
        // pacman loses since he hasn't eaten all the dots by now
        GameOver(WINNER_GHOST);
    }

    // needs to be public since event module on ui element calls this (not C# event)
    public void HomeMenu_OnPointerClick() {
        OnGameRestart();
    }

    /* END EVENT CALLBACKS */
}