using UnityEngine;

public class Pacman : MonoBehaviour {
    public int numLives = 3;
    private static float MOVE_SPEED = 1f;
    public float currentSpeed;

    private Rigidbody rb;
    private float[] vel = new float[3];


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateVelocity();
    }

    void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_onGameStateChanged;
    }

    /*
     * callback for gamemanager pause
     */
    private void GameManager_onGameStateChanged(bool isPaused)
    {
        currentSpeed = isPaused ? 0 : MOVE_SPEED;
    }

    void updateVelocity()
    {
        // the transform is rotated or something
        vel[0] = gameObject.transform.forward.z * currentSpeed;
        vel[1] = gameObject.transform.forward.y * currentSpeed;
        vel[2] = gameObject.transform.forward.x * currentSpeed;

        rb.velocity = new Vector3(vel[0], vel[1], vel[2]);
    }

    /**
     * Rough AI
     */
    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name.Equals("map"))
        {
            gameObject.transform.Rotate(Vector3.right);
        }
    }
}
