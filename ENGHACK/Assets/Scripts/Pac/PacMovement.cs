using UnityEngine;
using System.Collections.Generic;

public class PacMovement : ComputerMovementAI {

    Grid mapGrid;

	// Use this for initialization
	protected override void OnStart () {
        goal = findClosestDot("");
        mapGrid = GameObject.Find("map").GetComponent<Grid>();
    }

    protected override void UpdateGoal()
    {
        findPath();
    }

    /**
     * calculates path from pacman's position to closest dot, avoiding ghosts
     */
    private List<Cell> findPath()
    {
        // Breadth first search, baby!
        PriorityQueue<Cell> frontier = new PriorityQueue<Cell>();
        Cell start = mapGrid.CellFromWorldPoint(transform.position);
        start.cost = 0;
        frontier.Add(start);
        IDictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
        cameFrom.Add(start, null);
        IDictionary<Cell, int> costSoFar = new Dictionary<Cell, int>();
        costSoFar.Add(start, 0);

        // find closest four dots so we don't search the entire map, but we don't make pacman run into ghosts
        int dotsToProcess = 4;
        Cell goal = null;
        while (frontier.Count > 0 && dotsToProcess > 0)
        {
            Cell current = frontier.Remove();
            // if we find a cell with a dot in it, compare it with the current closest dot
            if (mapGrid.CellHasDot(current))
            {
                dotsToProcess--;
                if (goal == null || current.cost < goal.cost)
                {
                    goal = current;
                }
            }
            // check cell neighbours to add to frontier
            foreach (var next in mapGrid.GetCellNeighbours(current))
            {
                int newCost = costSoFar[current] + next.cost;
                // if next not in cost_so_far or new_cost < cost_so_far[next]:
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    frontier.Add(next);
                    cameFrom[next] = current;
                }
               
                if (!cameFrom.ContainsKey(next))
                {
                    frontier.Add(next);
                    cameFrom.Add(next, current);
                }
            }
        }
        return ExtractPath(start, goal, cameFrom);
    }

    /**
     * returns sequence of cells from pacman's position cell to an end
     */
    private List<Cell> ExtractPath(Cell _start, Cell _end, IDictionary<Cell, Cell> _cameFrom)
    {
        Cell current = _end;
        List<Cell> path = new List<Cell>();
        path.Add(current);
        while (current != _start) {
            current = _cameFrom[current];
            path.Add(current);
        //path.append(start) # optional
        //path.reverse() # optional
        }
        return path;
    }

    /***************** BELOW IS FOR GREEDY DOTFINDING ******************/

    /**
     * look for next dot to eat
     * @param eaten Name of dot that has just been eaten
     */
    public void HuntNextDot(string eaten)
    {
        goal = findClosestDot(eaten);
        // more accurate dot finding
        goal.position = new Vector3(goal.position.x, goal.position.y - 1, goal.position.z);
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
}
