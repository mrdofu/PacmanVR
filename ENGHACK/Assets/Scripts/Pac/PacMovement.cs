using UnityEngine;
using System.Collections;

public class PacMovement : MonoBehaviour {

    [SerializeField]
    NavMeshAgent navAgent;
    public Transform t;

	// Use this for initialization
	void Start () {
        navAgent.SetDestination(t.position);
        navAgent.Stop();
	}
	
    void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_onGameStateChanged;
    }

    /*
     * callback for gamemanager pause
     */
    private void GameManager_onGameStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            navAgent.Stop();
        } else
        {
            navAgent.Resume();
        }
    }
}
