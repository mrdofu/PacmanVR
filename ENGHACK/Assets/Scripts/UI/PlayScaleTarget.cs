using UnityEngine;
using System;
using VRStandardAssets.Utils;

public class PlayScaleTarget : ScaleTarget {
    [SerializeField]
    private SelectionRadial selectionRad;

    private GameManager gameManager;
    
    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	void OnEnable()
    {
        selectionRad.OnSelectionComplete += UnPause;
        selectionRad.OnSelectionComplete += Disable;
    }

    void OnDisable()
    {
        selectionRad.OnSelectionComplete -= UnPause;
    }

    public static Action OnPlayButtonComplete;

    /**
     * unpauses gameManager
     */
    void UnPause()
    {
        OnPlayButtonComplete();
    }

    /**
     * deactivates startUI game object and fades it away
     */
    void Disable()
    {
        // TODO: dissolve animation for StartUI
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
