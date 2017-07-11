using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private static float MOVE_SPEED = 1f;
    private float currentSpeed;
    [SerializeField]
    private GameObject playerHead;
    private Rigidbody rb;
    private float[] vel = new float[3]; //[0] = x, [1] = y, [2] = z

    private GameManager gameManager;
    
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentSpeed = 0;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    void OnEnable()
    {
        GameManager.OnGamePaused += GameManager_onGamePaused;
        GameManager.OnGamePlayed += GameManager_onGamePlayed;
    }

    void GameManager_onGamePaused()
    {
        currentSpeed = 0;
    }

    void GameManager_onGamePlayed()
    {
        currentSpeed = MOVE_SPEED;
    }

    // TODO: implement press to stop
    bool requestStop()
    {
        //Press and hold to request stop
        return Input.touchCount != 0;
    }

	// Update is called once per frame
	void Update ()
    {
        vel[0] = playerHead.transform.forward.x * currentSpeed;
        vel[1] = playerHead.transform.forward.y * currentSpeed;
        vel[2] = playerHead.transform.forward.z * currentSpeed;

        rb.velocity = new Vector3(vel[0], vel[1], vel[2]);
	}
}
