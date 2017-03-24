using UnityEngine;
using System.Collections;

public class PacMovement : MonoBehaviour {

    [SerializeField]
    NavMeshAgent navAgent;
    Transform goal;

	// Use this for initialization
	void Start () {
        goal = findClosestDot("");
        navAgent.SetDestination(goal.position);
        navAgent.Stop();
	}

    /**
     * look for next dot to eat
     * @param eaten Name of dot that has just been eaten
     */
    public void HuntNextDot(string eaten)
    {
            goal = findClosestDot(eaten);
            navAgent.SetDestination(goal.position);
    }

    /**
     * returns next closest dot transform for navigation
     * @param eaten Name of dot that has just been eaten
     */
    private Transform findClosestDot(string eaten)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Dot");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && !go.name.Equals(eaten))
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform;
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
