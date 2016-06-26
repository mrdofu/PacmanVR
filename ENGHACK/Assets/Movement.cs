using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float velocity;
    public GameObject mainCam;
    private Rigidbody rb;
     

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float vel_x = mainCam.transform.forward.x;
        rb.velocity = new Vector3(vel_x, mainCam.transform.forward.y, mainCam.transform.forward.z);
	}
}
