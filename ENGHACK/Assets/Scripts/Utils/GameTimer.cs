using UnityEngine;
using System;

/**
 * In-game timer that follows rules of pause, play, etc
 */
public class GameTimer : MonoBehaviour {

    public event Action OnTimerComplete;

    public float TimeLeft { get; private set; }

    [SerializeField]
    private float timerLength;
    private bool timerActive = false;

    void Update() {
        // can be paused, in which case, don't count time
        if (GameManager.CurrentState == GameManager.GameState.Play && timerActive) {
            if (TimeLeft > 0) {
                TimeLeft -= Time.deltaTime;
            } else {
                timerActive = false;
                OnTimerComplete();
            }
        }
    }

    public void ResetTimer() {
        TimeLeft = timerLength;
        timerActive = true;
    }
}
