using UnityEngine;
using System.Collections;

public class LookDownMenu : MonoBehaviour {

	public LookDownElement[] lookDownElements;
    
	float targetMenuAlpha;
	float currentMenuAlpha;
	float alphaMenuSpeed;
	FixedMenu menu;

    Transform vrHead;

	void Start() {
        menu = GetComponent<FixedMenu>();
        vrHead = GameObject.Find("GvrMain").transform.Find("Head");

        for (int i = 0; i < lookDownElements.Length; i++)
        {
            LookDownElement el = lookDownElements[i];
            Color c = el.text.color;
            c.a = el.currentAlpha * currentMenuAlpha;
            el.text.color = c;
            el.initialScale = el.iconTransform.localScale;
            el.iconTransform.localScale = el.initialScale + 0.2f * el.currentAlpha * el.initialScale;

            Color col = el.plane.material.color;
            col.a = currentMenuAlpha;
            el.plane.material.color = col;
        }
    }

    void Update() {
        if (vrHead.rotation.eulerAngles.x > 22 && vrHead.rotation.eulerAngles.x < 90)
        {
            targetMenuAlpha = 1;
        } else
        {
            targetMenuAlpha = 0;
        }

		bool updateAll = false;
		if (currentMenuAlpha != targetMenuAlpha){
			float menuSpringSpeed = 0.2f;
			if (targetMenuAlpha < currentMenuAlpha){
				menuSpringSpeed = 0.1f;
			}

			currentMenuAlpha = Smoothing.SpringSmooth (currentMenuAlpha, targetMenuAlpha, ref alphaMenuSpeed, menuSpringSpeed, Time.deltaTime);
			if (Mathf.Abs(currentMenuAlpha - targetMenuAlpha) < 0.005f){
				currentMenuAlpha = targetMenuAlpha;
			}
			updateAll = true;
		}

		if (currentMenuAlpha == 0){
			menu.Reset();
		}

        for (int i = 0; i < lookDownElements.Length; i++)
        {
            LookDownElement el = lookDownElements[i];
            
            if (el.currentAlpha != el.targetAlpha || updateAll)
            {

                float springSpeed = 0.2f;
                if (el.targetAlpha < el.currentAlpha)
                {
                    springSpeed = 0.1f;
                }

                el.currentAlpha = Smoothing.SpringSmooth(el.currentAlpha, el.targetAlpha, ref el.alphaSpeed, springSpeed, Time.deltaTime);
                if (Mathf.Abs(el.currentAlpha - el.targetAlpha) < 0.005f)
                {
                    el.currentAlpha = el.targetAlpha;
                }
                Color c = el.text.color;
                c.a = el.currentAlpha * currentMenuAlpha;
                el.text.color = c;

                el.iconTransform.localScale = el.initialScale + 0.2f * el.currentAlpha * el.initialScale;
                el.textTransform.localPosition = el.currentAlpha * el.endTextPosition + (1 - el.currentAlpha) * el.initialTextPosition;
            }
            Color col = el.plane.material.color;
            col.a = currentMenuAlpha;
            el.plane.material.color = col;
        }

    }

    [System.Serializable]
	public class LookDownElement{
		public TextMesh text;
		public Transform textTransform;
        //public GazeTarget target;
        public MeshRenderer plane;
		public Transform iconTransform;
		public float targetAlpha;
		public float currentAlpha;
		public float alphaSpeed;
		public Vector3 initialScale;

		public readonly Vector3 initialTextPosition = new Vector3(0, 0.19f, 0);
		public readonly Vector3 endTextPosition = new Vector3(0, 0.23f, 0);
	}
}
