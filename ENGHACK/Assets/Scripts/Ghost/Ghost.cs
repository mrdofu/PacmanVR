using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool isVulnerable { get; set; }

    public const float MAX_VULNERABLE_TIME = 10f;      // maximum time a ghost can be vulnerable for
    private float vulnerableTimer = 0f;              // timer to count how long a ghost has been vulnerable for

    public static event Action OnEatsPacman;    // event for pacman/ ghost collision where pacman loses

    void Start()
    {
        isVulnerable = false;
    }

    void Update()
    {
        checkVulnerableTimer();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Pacman"))
        {
            if (isVulnerable)
            {
                // ghost dies
                // ghost respawns
                transform.position = GameObject.Find("ghostSpawn2").transform.position;
                // cancel the timer
                // ghost becomes invulnerable
                isVulnerable = false;
                // pacman is awarded points
            } else
            {
                OnEatsPacman();
            }
        }
    }

    /**
     * Ghosts become vulnerable to be eaten by pacman
     */
     public void setVulnerable()
    {
        isVulnerable = true;
    }

    private void checkVulnerableTimer()
    {
        // if a ghost becomes vulnerable, start counting
        if (isVulnerable)
        {
            vulnerableTimer += Time.deltaTime;
            if (vulnerableTimer >= MAX_VULNERABLE_TIME)
            {
                isVulnerable = false;
                vulnerableTimer = 0f;
            }
        }
    }
}