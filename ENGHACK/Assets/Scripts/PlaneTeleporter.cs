using UnityEngine;
using System.Collections;

public class PlaneTeleporter : MonoBehaviour {
    private GameObject[] ghostObjects;
    private GameObject pacmanObject;
    
	// Use this for initialization
	void Start () {
        ghostObjects = GameObject.FindGameObjectsWithTag("Ghost");
        pacmanObject = GameObject.FindGameObjectWithTag("Pacman");
	}

    // Update is called once per frame
	void Update () {
        foreach (var ghost in ghostObjects)
        {
            if (CheckOutOfMap(ghost))
            {
                Teleport(ghost);
            }
        }
	}

    /**
     * Checks if the gameobject is out of bounds of the map
     * @param go gameobject to check
     */
    bool CheckOutOfMap(GameObject go)
    {
        return go.transform.position.x < -14.5f ||
            go.transform.position.x > 14.5f;
    }

    /**
     * Teleports gameobject to other side of the map, assuming they left through the sides
     * @param go gameobject to check
     */
    void Teleport(GameObject go)
    {
        Vector3 newPosition = go.transform.position;
        if (newPosition.x < 0)
        {
            newPosition.x += 0.5f;
        } else
        {
            newPosition.x -= 0.5f;
        }
        newPosition.x *= -1;
        go.transform.position = newPosition;
    }
	

}
