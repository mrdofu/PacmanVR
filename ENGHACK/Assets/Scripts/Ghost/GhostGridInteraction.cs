using UnityEngine;
using System.Collections.Generic;

public class GhostGridInteraction : MonoBehaviour {

    Grid mapGrid;

    void Start () {
        mapGrid = GameObject.Find("map").GetComponent<Grid>();
    }

    void Update () {
        addCellCosts();
    }

    /**
     * Adds cell costs to cells surrounding ghost for pacman's pathfinding
     */
    private void addCellCosts()
    {
        // reset the cells in a greater area than a ghost's cost area so that cells the ghost leaves are reset
        Cell ghostCell = mapGrid.CellFromWorldPoint(transform.position);
        // TODO: reset
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
}
