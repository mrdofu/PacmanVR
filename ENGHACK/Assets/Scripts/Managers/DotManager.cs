using UnityEngine;

public class DotManager : MonoBehaviour {

    // Use this for initialization
    void Start() {
        SpawnDots();
    }

    private void OnEnable() {
        GameManager.OnGameRestart += SpawnDots;
    }

    private void OnDisable() {
        GameManager.OnGameRestart -= SpawnDots;
    }

    /**
     * Instantiates dot prefabs on dotspawnpoints
     */
    void SpawnDots() {
        GameObject dotManager = GameObject.Find("DotManager");
        // get dot spawns
        GameObject[] dotSpawnGOs = GameObject.FindGameObjectsWithTag("DotSpawn");
        // spawn appropriate dot
        foreach (var dotSpawnGO in dotSpawnGOs) {
            SpawnPoint dotSpawn = dotSpawnGO.GetComponent<SpawnPoint>();
            dotSpawn.InstantiatePrefab(dotManager.transform);
        }
    }
}
