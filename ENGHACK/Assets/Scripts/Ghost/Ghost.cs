using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool isVulnerable { get; set; }

    void Start()
    {
        isVulnerable = false;
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

    /* 
     * When a ghost goes vulnerable, a timer starts. If the ghost is vulnerable by the end of the timer,
     * he no longer will be
     */
     public void setVulnerable()
    {
        isVulnerable = true;

    }
}