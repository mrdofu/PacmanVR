using System;
using UnityEngine;

public class Pacman : MonoBehaviour {
    public const int MAX_LIVES = 3;
    private int numLives;

    public static event Action OnLoseLife;
    public static event Action OnLoseAllLives;

    [SerializeField]
    Transform pacSpawn;

    void Start()
    {
        numLives = MAX_LIVES;
    }

    void OnEnable()
    {
        GameManager.OnGameReset += GameManager_OnGameReset;
        GameManager.OnGameRestart += GameManager_OnGameRestart;
        Ghost.OnEatsPacman += Ghost_OnEatsPacman;
    }

    void OnDisable()
    {
        GameManager.OnGameReset -= GameManager_OnGameReset;
        GameManager.OnGameRestart -= GameManager_OnGameRestart;
        Ghost.OnEatsPacman -= Ghost_OnEatsPacman;
    }

    /* EVENT CALLBACKS */

    private void Ghost_OnEatsPacman()
    {
        numLives--;
        if (numLives > 0)
        {
            OnLoseLife();
        } else
        {
            OnLoseAllLives();
        }
    }

    private void GameManager_OnGameReset() {
        // resets pacman's position
        transform.position = pacSpawn.position;
    }

    private void GameManager_OnGameRestart() {
        transform.position = pacSpawn.position;
        numLives = MAX_LIVES;
    }

    /* END EVENT CALLBACKS */
}
