using System;
using UnityEngine;

public class Pacman : MonoBehaviour {
    public const int MAX_LIVES = 3;
    private int numLives;
    [SerializeField]
    private GameObject pacSpawn;

    public static event Action OnLoseLife;
    public static event Action OnLoseAllLives;

    // Use this for initialization
    void Start()
    {
        numLives = MAX_LIVES;
    }

    void OnEnable()
    {
        Ghost.OnEatsPacman += Ghost_OnEatsPacman;
    }

    void OnDisable()
    {
        Ghost.OnEatsPacman -= Ghost_OnEatsPacman;
    }

    // Update is called once per frame
    void Update()
    {

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

    private void GameManager_OnGameReset()
    {
        transform.position = pacSpawn.transform.position;
    }

    /* END EVENT CALLBACKS */
}
