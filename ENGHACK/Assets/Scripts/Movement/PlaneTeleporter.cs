using UnityEngine;
using System.Collections.Generic;

public class PlaneTeleporter : MonoBehaviour {
    private const float map_half_width = 11f;

    private IList<GameObject> characters;
    
	// Use this for initialization
	void Start () {
        characters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ghost"));
        characters.Add(GameObject.FindGameObjectWithTag("Pacman"));
	}

    // Update is called once per frame
	void Update () {
        foreach (var agent in characters) {
            if (CheckOutOfMap(agent)) {
                Teleport(agent);
            }
        }
	}

    /**
     * Checks if the gameobject is out of bounds of the map
     * @param go gameobject to check
     */
    bool CheckOutOfMap(GameObject go)
    {
        return go.transform.position.x < -map_half_width ||
            go.transform.position.x > map_half_width;
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

        if (go.name.Equals("Player")) {
            go.GetComponent<PlayerMovement>().teleport(newPosition);
        } else {
            go.transform.position = newPosition;
        }
    }
	

}
