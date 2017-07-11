using UnityEngine;

public abstract class ComputerMovementAI : MonoBehaviour {

    [SerializeField]
    private NavMeshAgent navAgent;
    protected Transform goal;

    // Use this for initialization
    void Start()
    {
        OnStart();
        UpdateGoal();
        navAgent.SetDestination(goal.position);
        navAgent.Stop();
    }

    protected abstract void OnStart();
    /**
     * must set goal to non-null
     */
    protected abstract void UpdateGoal();

    void Update()
    {
        navAgent.SetDestination(goal.position);
    }

    void OnEnable()
    {
        GameManager.OnGamePaused += GameManager_onGamePaused;
        GameManager.OnGamePlayed += GameManager_onGamePlayed;
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
