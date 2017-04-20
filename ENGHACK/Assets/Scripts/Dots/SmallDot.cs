using UnityEngine;
using System.Collections;

public class SmallDot : MonoBehaviour {

    /**
     * If pacman eats a small dot, points are awarded, check if there are any dots left, and the dot is destroyed
     * @param other - the other object that collided with this
     */
    void OnTriggerEnter(Collider other) {
        GameObject pacmanGameObj = GameObject.FindGameObjectWithTag("Pacman");
        if (other.gameObject == pacmanGameObj)
        {
            // TODO: award points
            Destroy(this.gameObject);
            // TODO: check for remaining dots
            Transform gameManager = transform.parent;
            if (gameManager == null) {
                Debug.Log("Dot is not a child of GameManager");
                return;
            }
            else if (gameManager.childCount <= 0)
            {
                gameManager.GetComponent<GameManager>().GameOver(GameManager.WINNER_PACMAN);
            }

            // make pacman recalculate next destination
            pacmanGameObj.GetComponent<PacMovement>().HuntNextDot(gameObject.name);
        }
    }
}
