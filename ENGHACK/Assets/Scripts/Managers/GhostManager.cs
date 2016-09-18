using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public GameObject ghost;                // The enemy prefab to be spawned.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.


    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        Spawn();
    }


    void Spawn()
    {
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        int numSpawnPoints = spawnPoints.Length;
        for (int i = 0; i < numSpawnPoints; i++)
        {
            Instantiate(ghost, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
