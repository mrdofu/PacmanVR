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
        Pause, Play, Over
    };
    public static GameState CurrentState { get; private set; }

    private GameTimer matchTimer;
    private GameObject[] hudArray;

    void Awake() {
        matchTimer = gameObject.GetComponent<GameTimer>();
    }

    void Start() {
        OnGamePaused();
        hudArray = GameObject.FindGameObjectsWithTag("HUD");
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

    void Update() {
        if (CurrentState == GameState.Play) {
            foreach (var hudObj in hudArray) {
                Text gameStatusText = hudObj.transform.Find("gameStatusText").GetComponent<Text>();
                gameStatusText.text = matchTimer.TimeLeft.ToString("n2");
            }
        }
    }

    /**
     * To be called after all pacman lives are lost or after time runs out or all dots are consumed
     * @param winner 0 if pacman wins, 1 if ghosts win
     */
    void GameOver(int winner) {
        CurrentState = GameState.Over;
        // Determine winner based on parameter
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
        matchTimer.ResetTimer();
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