using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour {

    private void OnEnable() {
        GameManager.OnGamePlayed += DisableChildren;
        GameManager.OnGamePaused += ActivateChildren;
    }

    private void OnDisable() {
        GameManager.OnGamePlayed -= DisableChildren;
        GameManager.OnGamePaused -= ActivateChildren;
    }

    /**
     * activates startUI children
     */
    void ActivateChildren() {
        SetChildrenActive(true);
    }

    /**
     * deactivates startUI children and fades it away
     */
    void DisableChildren() {
        // TODO: dissolve animation for StartUI
        SetChildrenActive(false);
    }

    /**
     * helper for activating or deactivating children gameobjects
     */
    void SetChildrenActive(bool active) {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(active);
        }
    }

}
