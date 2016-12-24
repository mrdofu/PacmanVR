using UnityEngine;
using System.Collections;

/**
 * Look down menu animations
 */
public class ScaleTarget : MonoBehaviour {

    [SerializeField]
    Transform scaleTargetTransform;

    public bool IsTarget { get; set; }

    float scale = 0;
    float scaleTarget = 0;
    float scaleSpeed = 0;

    public float hoverScale = 1.2f;

    Vector3 baseScale;
    Vector3 targetScale;

    void Start () {
        IsTarget = false;

        if (scaleTargetTransform != null)
        {
            baseScale = scaleTargetTransform.localScale;
            targetScale = baseScale * hoverScale;
        }
    }
	
	void Update () {
        // scale enlarging
        scale = Smoothing.SpringSmooth(scale, scaleTarget, ref scaleSpeed, 0.1f, Time.deltaTime);
        if (scaleTargetTransform != null)
        {
            ///wtf unity
            if (baseScale == Vector3.zero)
            {
                baseScale = scaleTargetTransform.localScale;
                targetScale = baseScale * hoverScale;
            }
            scaleTargetTransform.localScale = scale * targetScale + (1 - scale) * baseScale;
        }
        

    }

    /**
     * Callback for when the camera's raycaster hits this gameobject's collider
     */
    public void onPointerEnter()
    {
        scaleTarget = 1;
        IsTarget = true;
    }

    /**
     * Callback for when the camera's raycaster stops colliding with this gameobject's collider
     */
    public void onPointerExit()
    {
        scaleTarget = 0;
        IsTarget = false;
    }
}