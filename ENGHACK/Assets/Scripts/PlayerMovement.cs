using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private static float MOVE_SPEED = 1f;
    public float currentSpeed;
    public GameObject mainCam;
    private Rigidbody rb;
    private float[] vel = new float[3]; //[0] = x, [1] = y, [2] = z
    
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentSpeed = 0;
	}

    
    bool requestStop()
    {
        //Press and hold to request stop
        return Input.touchCount != 0;
        
    }

	// Update is called once per frame
	void Update ()
    {
        if (requestStop())
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed = MOVE_SPEED;
        }

        vel[0] = mainCam.transform.forward.x * currentSpeed;
        vel[1] = mainCam.transform.forward.y * currentSpeed;
        vel[2] = mainCam.transform.forward.z * currentSpeed;
        

        rb.velocity = new Vector3(vel[0], vel[1], vel[2]);
	}
}
