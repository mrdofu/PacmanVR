using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float velocity = 1f;
    private float oldVelocity;
    public GameObject mainCam;
    private Rigidbody rb;
    private float[] vel = new float[3]; //[0] = x, [1] = y, [2] = z
    
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        oldVelocity = velocity;
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
            velocity = 0;
        }
        else
        {
            velocity = oldVelocity;
        }

        vel[0] = mainCam.transform.forward.x * velocity;
        vel[1] = mainCam.transform.forward.y * velocity;
        vel[2] = mainCam.transform.forward.z * velocity;
        

        rb.velocity = new Vector3(vel[0], vel[1], vel[2]);
	}
}
