using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool isVulnerable;

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
                // ghost respawns 
                // cancel the timer
                // ghost becomes invulnerable
                // pacman is awarded points
            }
            else
            {
                // pacman loses a life
                // everybody respawns
            }
        }
    }

    /* 
     * When a ghost goes vulnerable, a timer starts. If the ghost is vulnerable by the end of the timer,
     * he no longer will be
     */
}