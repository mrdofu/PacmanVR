using UnityEngine;
using System.Collections;

public class BigDot : Dot {
    GhostManager gm;

    protected override void Start() {
        base.Start();
        gm = GameObject.Find("GhostManager").GetComponent<GhostManager>();
    }
    /**
     * If pacman eats a big dot, the ghosts become vulnerable
     */
    protected override void OnEaten() {
        gm.SetGhostsVulnerable();
    }
}
