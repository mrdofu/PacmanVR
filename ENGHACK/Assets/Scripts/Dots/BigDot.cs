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
            // TODO: award points
            Destroy(this.gameObject);

            GameObject.Find("GhostManager").GetComponent<GhostManager>().SetGhostsVulnerable();
            // TODO: check for remaining dots
            Transform gameManager = transform.parent;
            if (gameManager == null)
            {
                Debug.Log("Dot is not a child of GameManager");
                return;
            }
            else
            {
                if (gameManager.childCount <= 0)
                {
                    gameManager.GetComponent<GameManager>().GameOver(GameManager.WINNER_PACMAN);
                }
            }

            // make pacman recalculate next destination
        }
    }

}
