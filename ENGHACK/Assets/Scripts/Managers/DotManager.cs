using UnityEngine;

public class DotManager : MonoBehaviour {

    // Use this for initialization
    void Start() {
        SpawnDots();
    }


    /**
     * Instantiates dot prefabs on dotspawnpoints
     */
    void SpawnDots() {
        GameObject gameManager = GameObject.Find("GameManager");
        // get dot spawns
        GameObject[] dotSpawnGOs = GameObject.FindGameObjectsWithTag("DotSpawn");
        // spawn appropriate dot
        foreach (var dotSpawnGO in dotSpawnGOs) {
            SpawnPoint dotSpawn = dotSpawnGO.GetComponent<SpawnPoint>();
            dotSpawn.InstantiatePrefab(gameManager.transform);
        }
    }
}
