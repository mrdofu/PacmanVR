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
                // ghost respawns invulnerable
                // pacman is awarded points
            }
            else
            {
                // pacman loses a life
                // everybody respawns
            }
        }
    }

}