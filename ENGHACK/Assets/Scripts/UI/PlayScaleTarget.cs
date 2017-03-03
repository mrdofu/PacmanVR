using UnityEngine;
using System.Collections;
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
    }

    void OnDisable()
    {
        selectionRad.OnSelectionComplete -= UnPause;
    }

    void UnPause()
    {
        gameManager.GamePaused = false;
    }
}
