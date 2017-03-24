using UnityEngine;

public class Pacman : MonoBehaviour {
    public int numLives = 3;
    
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
