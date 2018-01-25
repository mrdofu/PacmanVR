using UnityEngine;

public abstract class ComputerMovementAI : MonoBehaviour {

    [SerializeField]
    private NavMeshAgent navAgent;
    private Vector3 goal;

    // Use this for initialization
    void Start()
    {
        OnStart();
    }

    protected abstract void OnStart();
    /**
     * must set goal to non-null
     */
    protected abstract Vector3 UpdateGoal();

    void Update()
    {
        goal = UpdateGoal();
        navAgent.SetDestination(goal);
    }

    void OnEnable()
    {
        GameManager.OnGamePaused += GameManager_onGamePaused;
        GameManager.OnGamePlayed += GameManager_onGamePlayed;
    }

    void OnDisable()
    {
        GameManager.OnGamePaused -= GameManager_onGamePaused;
        GameManager.OnGamePlayed -= GameManager_onGamePlayed;
    }

    /*
     * callback for gamemanager pause
     */
    private void GameManager_onGamePaused()
    {
        navAgent.Stop();
    }

    private void GameManager_onGamePlayed()
    {
        navAgent.Resume();
    }
}
