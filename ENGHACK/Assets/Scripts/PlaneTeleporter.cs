using UnityEngine;
using System.Collections;

public class PlaneTeleporter : MonoBehaviour {
    public GameObject character;
    private Rigidbody rb;
    
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
    
    bool outOfMap()
    {
        return character.transform.position.x < -14.5f ||
            character.transform.position.x > 14.5f;
    }
    void teleport()
    {
        Vector3 newPosition = character.transform.position;
        newPosition.x *= -1;
        rb.MovePosition(newPosition);

    }
	
	// Update is called once per frame
	void Update () {
        if (outOfMap())
        {
            teleport();
        }
	    
	}
}
