using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    [SerializeField]
    NavMeshAgent navAgent;
    GameObject pacGo;
    Transform goal;

    Grid mapGrid;

    // Use this for initialization
    void Start()
    {
        pacGo = GameObject.Find("PACMAN");
        goal = pacGo.transform;
        navAgent.SetDestination(goal.position);
        navAgent.Stop();
    }

    void Update()
    {
        goal = pacGo.transform;
        navAgent.SetDestination(goal.position);
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
        }
        else
        {
            navAgent.Resume();
        }
    }
}
