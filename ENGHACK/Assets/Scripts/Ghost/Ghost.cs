using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool isVulnerable { get; set; }

    public const float MAX_VULNERABLE_TIME = 10f;      // maximum time a ghost can be vulnerable for
    private float vulnerableTimer = 0f;              // timer to count how long a ghost has been vulnerable for

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
            }
            else
            {
                // pacman loses a life
                Pacman pacmanScript = col.gameObject.GetComponent<Pacman>();
                pacmanScript.numLives -= 1;
                // if he still has lives, everybody respawns
                if (pacmanScript.numLives > 0)
                {

                }
                // else, gosts win
                else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().GameOver(GameManager.WINNER_GHOST);
                }
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