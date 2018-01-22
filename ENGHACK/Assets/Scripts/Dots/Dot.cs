using UnityEngine;
using System;

public abstract class Dot : MonoBehaviour {
    GameObject pacmanGameObj;

    public static event Action OnAllDotsEaten;

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
            // check for remaining dots
            Transform gameManager = transform.parent;
            if (gameManager == null) {
                Debug.Log("Dot is not a child of GameManager");
                return;
            } else if (gameManager.childCount <= 0) {
                OnAllDotsEaten();
            }
            OnEaten();
            // make pacman recalculate next destination
        }
    }
}
