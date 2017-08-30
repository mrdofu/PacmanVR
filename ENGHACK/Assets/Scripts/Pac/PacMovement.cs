using UnityEngine;
using System.Collections.Generic;

public class PacMovement : ComputerMovementAI {

    Grid mapGrid;

    private List<Cell> path;

	// Use this for initialization
	protected override void OnStart () {
        mapGrid = GameObject.Find("map").GetComponent<Grid>();
    }

    protected override Vector3 UpdateGoal()
    {
        Vector3 goal;
        path = findPath();
        if (path.Count > 0)
        {
            goal = path[0].worldPosition;
            goal = new Vector3(goal.x, goal.y - 1, goal.z);
        } else
        {
            goal = transform.position;
        }
        return goal;
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

    /**
     * Helps see pathfinding on grid
     */
    void OnDrawGizmosSelected()
    {
        if (path != null)
        {
            foreach (Cell c in path)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(c.worldPosition, Grid.CELL_RADIUS);
            }
        }
    }
}
