using UnityEngine;
using System.Collections.Generic;

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

        mapGrid = GameObject.Find("map").GetComponent<Grid>();
    }

    void Update()
    {
        goal = pacGo.transform;
        navAgent.SetDestination(goal.position);
        addCellCosts();
    }

    void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_onGameStateChanged;
    }

    /**
     * Adds cell costs to cells surrounding ghost for pacman's pathfinding
     */
    private void addCellCosts()
    {
        // reset the cells in a greater area than a ghost's cost area so that cells the ghost leaves are reset
        Cell ghostCell = mapGrid.CellFromWorldPoint(transform.position);
        // reset
        // add costs
        IDictionary<Cell, bool> visited = new Dictionary<Cell, bool>();
        visited.Add(ghostCell, true);
        Queue<Cell> toVisit = new Queue<Cell>(mapGrid.GetCellNeighbours(ghostCell));
        for (int dist = 1, range = 4; dist < range; dist++)
        {
            int cellCost = (range - dist) * (range - dist);
            for (int i = 0, size = toVisit.Count; i < size; i++)
            {
                Cell current = toVisit.Dequeue();
                current.cost = cellCost;
                visited.Add(current, true);
                List<Cell> nextDistNeighbs = mapGrid.GetCellNeighbours(current);
                foreach (Cell c in nextDistNeighbs)
                {
                    if (!visited.ContainsKey(c))
                    {
                        toVisit.Enqueue(c);
                    }
                }
            }
        }
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
