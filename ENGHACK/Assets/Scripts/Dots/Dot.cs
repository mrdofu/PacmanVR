using UnityEngine;
using System.Collections;

public abstract class Dot : MonoBehaviour {
    GameObject pacmanGameObj;

    protected virtual void Start() {
        pacmanGameObj = GameObject.FindGameObjectWithTag("Pacman");
    }

    /**
     * template method for behaviour after dot is eaten
     */
    protected abstract void OnEaten();

    /**
      * If pacman eats a dot, points are awarded, check if there are any dots left, and the dot is destroyed
      * @param other - the other object that collided with this
      */
    void OnTriggerEnter(Collider other) {
        if (other.gameObject == pacmanGameObj) {
            // TODO: award points
            Destroy(this.gameObject);
            // TODO: check for remaining dots
            Transform gameManager = transform.parent;
            if (gameManager == null) {
                Debug.Log("Dot is not a child of GameManager");
                return;
            } else if (gameManager.childCount <= 0) {
                gameManager.GetComponent<GameManager>().GameOver(GameManager.WINNER_PACMAN);
            }
            OnEaten();
            // make pacman recalculate next destination
        }
    }
}
