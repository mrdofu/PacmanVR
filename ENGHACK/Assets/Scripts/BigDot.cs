using UnityEngine;
using System.Collections;

public class BigDot : MonoBehaviour
{
    /**
     * If pacman eats a big dot, the ghosts become vulnerable, points are awarded, check for remaining dots, and the dot is destroyed
     * @param other - the other object that collided with this
     */
    void OnTriggerEnter(Collider other)
    {
        GameObject pacmanGameObj = GameObject.FindGameObjectWithTag("Pacman");
        if (other.gameObject == pacmanGameObj)
        {
            Destroy(this.gameObject);
            // TODO: ghosts vulnerable
            GameObject.Find("GhostManager").GetComponent<GhostManager>().SetGhostsVulnerable();
            // TODO: award points
            // TODO: check for remaining dots
        }
    }

}
