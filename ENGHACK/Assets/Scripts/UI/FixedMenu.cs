using UnityEngine;
using System.Collections;

public class FixedMenu : MonoBehaviour {
	float currentYaw = 0;
	public float maxYaw = 15;
	float previousYaw;

	public bool lockPitch = false;
	public float pitch = 0;

	public bool counterRotateUI = false;
	public Transform UI;

    Transform vrHead;

	public float Pitch{
		get{
			return pitch;
		}
		set{
			pitch = value;
		}
	}

	void Start(){
        vrHead = GameObject.Find("Player").transform.Find("Head");
        previousYaw = vrHead.rotation.eulerAngles.y;
	}

	public void Reset(){
		previousYaw = vrHead.rotation.eulerAngles.y;
		currentYaw = 0;
	}

	void LateUpdate () {
		float deviceYaw = vrHead.rotation.eulerAngles.y;
		float devicePitch = -vrHead.rotation.eulerAngles.x;
		
		float deltaYaw = Mathf.DeltaAngle(deviceYaw, previousYaw);

		float targetYaw = currentYaw + deltaYaw;
		
		currentYaw = Mathf.Clamp(targetYaw, -maxYaw,maxYaw);
		
		transform.position = vrHead.position;


		previousYaw = deviceYaw;
		Quaternion menuPitch = Quaternion.AngleAxis(pitch, Vector3.right);
		Quaternion deviceRotation = Quaternion.AngleAxis(deviceYaw, Vector3.up);
		Quaternion devicePitchRotation = Quaternion.AngleAxis(-devicePitch, Vector3.right);
		///TODO: try an ease on this to smooth out the edges

		if(!lockPitch){
			transform.localRotation = deviceRotation * Quaternion.AngleAxis( currentYaw, Vector3.up) * menuPitch;
		}
		else{
			transform.localRotation = deviceRotation * Quaternion.AngleAxis( currentYaw, Vector3.up) * devicePitchRotation * menuPitch;
		}

		if(counterRotateUI){
			UI.localRotation = Quaternion.AngleAxis(-pitch, Vector3.right);
		}
	}
}
