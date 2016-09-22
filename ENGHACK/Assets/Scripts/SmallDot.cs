﻿using UnityEngine;
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
                return;
            }

            if (gameManager.childCount == 0)
            {
                // game over
                
            }
        }
    }

}
