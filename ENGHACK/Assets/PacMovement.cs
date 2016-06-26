using UnityEngine;

public class PacMovement : MonoBehaviour {
    public float speed = 1f;
    private Rigidbody rb;
    private float[] vel = new float[3];


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vel[0] = gameObject.transform.forward.x * speed;
        vel[1] = gameObject.transform.forward.y * speed;
        vel[2] = gameObject.transform.forward.z * speed;

        rb.velocity = new Vector3(vel[0], vel[1], vel[2]);
    }
}
