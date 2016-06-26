using UnityEngine;
using System.Collections;

public class MapPhysics : MonoBehaviour {

    private PhysicMaterial mapPhysics;
	void Start () {
        mapPhysics = GetComponent<PhysicMaterial>();
        mapPhysics.dynamicFriction = 0;
        mapPhysics.staticFriction = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
