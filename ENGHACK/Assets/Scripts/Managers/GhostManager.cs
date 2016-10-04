using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public GameObject ghostPrefab;                // The enemy prefab to be spawned.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

    private GameObject[] ghostObjects;

    void Start()
    {
        // Spawn ghosts
        SpawnGhosts();
        InitializeGhosts();
    }

    /** 
     * Create an instance of all ghosts
     */
    void SpawnGhosts()
    {
        int numSpawnPoints = spawnPoints.Length;
        for (int i = 0; i < numSpawnPoints; i++)
        {
            Instantiate(ghostPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }

    /**
     * Keeps all logic to deal with initial ghost stuff
     */
    void InitializeGhosts()
    {
        // keep a reference of the ghosts
        ghostObjects = GameObject.FindGameObjectsWithTag("Ghost");
    }
    
    public GameObject[] getGhostObjects()
    {
        return ghostObjects;
    }

    /**
     * To be called when pacman eats a big dot. Makes ghosts vulnerable for pacman to eat
     */
    public void SetGhostsVulnerable()
    {
        foreach (GameObject ghostObject in ghostObjects)
        {
            Ghost ghost = ghostObject.GetComponent<Ghost>();
            ghost.setVulnerable();
        }
    }
}
