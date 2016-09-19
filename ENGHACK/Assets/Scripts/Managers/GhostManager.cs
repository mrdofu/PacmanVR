using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public GameObject ghost;                // The enemy prefab to be spawned.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.


    void Start()
    {
        // Spawn ghosts
        Spawn();
    }

    // Create an instance of all ghosts
    void Spawn()
    {
        int numSpawnPoints = spawnPoints.Length;
        for (int i = 0; i < numSpawnPoints; i++)
        {
            Instantiate(ghost, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
