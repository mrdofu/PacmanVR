using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool isVulnerable { get; set; }

    public float maxVulnerableTime = 10f;      // maximum time a ghost can be vulnerable for
    private float vulnerableTimer;              // timer to count how long a ghost has been vulnerable for

    void Start()
    {
        isVulnerable = false;
    }

    void Update()
    {
        // if a ghost becomes vulnerable, start counting
        if (isVulnerable)
        {

        }
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
                // pacman is awarded points
            }
            else
            {
                // pacman loses a life
                Pacman pacmanScript = col.gameObject.GetComponent<Pacman>();
                pacmanScript.numLives -= 1;
                // if he still has lives, everybody respawns
                // else, game over
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
}