using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    GameObject player;
    Transform spawnpoint;

    private float speed = 0.0005f;
    private Vector3 movement;
    private int floorMask;
    private CharacterController playerCC;
    private Rigidbody playerRB;
    private bool stopRequested = (Input.touchCount % 2) == 1;

	// Use this for initialization
	void Start () {
        floorMask = LayerMask.GetMask("Floor");
        playerRB = GetComponent<Rigidbody>();
        playerCC = GetComponent<CharacterController>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!stopRequested)
        {
            
            Walk();
        }
    }

    void Walk() {
        playerRB.velocity = new Vector3();
    }
}
