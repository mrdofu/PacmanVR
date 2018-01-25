using UnityEngine;
using System;
using VRStandardAssets.Utils;

public class PlayScaleTarget : ScaleTarget {
    [SerializeField]
    private SelectionRadial selectionRad;

    public static Action OnPlayButtonComplete;

    void OnEnable()
    {
        selectionRad.OnSelectionComplete += SelectionRadial_OnSelectionComplete;
    }

    void OnDisable()
    {
        selectionRad.OnSelectionComplete -= SelectionRadial_OnSelectionComplete;
    }

    /* EVENT CALLBACKS */
    private void SelectionRadial_OnSelectionComplete() {
        OnPlayButtonComplete();
    }
    /* END EVENT CALLBACKS */
}
