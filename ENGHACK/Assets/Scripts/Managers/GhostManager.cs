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

    void OnEnable()
    {
        GameManager.OnGameReset += RespawnGhosts;
        GameManager.OnGameRestart += RespawnGhosts;
    }

    void OnDisable()
    {
        GameManager.OnGameReset -= RespawnGhosts;
        GameManager.OnGameRestart -= RespawnGhosts;
    }

    void Update()
    {
        AddCellCosts();
    }

    /**
     * Adds cell costs to area surrounding ghosts
     */
    private void AddCellCosts()
    {
        // reset for each AND THEN add costs for each, so that reset cost from one doesn't affect costs from others
        foreach (var ghost in ghostObjects)
        {
            GhostGridInteraction ggi = ghost.GetComponent<GhostGridInteraction>();
            ggi.resetGhostAreaCost();
        }
        foreach (var ghost in ghostObjects)
        {
            GhostGridInteraction ggi = ghost.GetComponent<GhostGridInteraction>();
            ggi.addCellCosts();
        }

    }

    /** 
     * Create an instance of all ghosts
     */
    void SpawnGhosts()
    {
        int numSpawnPoints = spawnPoints.Length;
        for (int i = 0; i < numSpawnPoints - 1; i++)
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

    /**
     * Respawns all ghosts from spawnpoints
     */
    public void RespawnGhosts() {
        for (int i = 0; i < ghostObjects.Length; i++)
        {
            GameObject ghostObj = ghostObjects[i];
            Vector3 targetPos = spawnPoints[i].transform.position;
            // blackout for player character
            if (ghostObj.name.Equals("Player")) {
                ghostObj.GetComponent<PlayerMovement>().teleport(targetPos);
            } else {
                ghostObj.transform.position = targetPos;
            }
        }
    }
}
