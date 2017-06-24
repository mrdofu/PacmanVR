using UnityEngine;
using System.Collections.Generic;

public class GhostGridInteraction : MonoBehaviour {

    public int DANGER_RANGE = 4;
    Grid mapGrid;

    void Start () {
        mapGrid = GameObject.Find("map").GetComponent<Grid>();
    }

    /**
     * Adds cell costs to cells surrounding ghost for pacman's pathfinding
     */
    public void addCellCosts()
    {
        Cell ghostCell = mapGrid.CellFromWorldPoint(transform.position);
        // add costs
        IDictionary<Cell, bool> visited = new Dictionary<Cell, bool>();
        visited.Add(ghostCell, true);
        Queue<Cell> toVisit = new Queue<Cell>(mapGrid.GetCellNeighbours(ghostCell));
        for (int dist = 1; dist < DANGER_RANGE; dist++)
        {
            int cellCost = (DANGER_RANGE - dist) * (DANGER_RANGE- dist);
            // dequeues cells from frontier and gives them a cost, adding neighbours to queue
            for (int i = 0, size = toVisit.Count; i < size; i++)
            {
                Cell current = toVisit.Dequeue();
                // in case ghosts are close to another, we want the greater cost to be reflected
                current.cost = Mathf.Max(cellCost, current.cost);
                if (!visited.ContainsKey(current))
                {
                    visited.Add(current, true);

                    List<Cell> nextDistNeighbs = mapGrid.GetCellNeighbours(current);
                    foreach (Cell c in nextDistNeighbs)
                    {
                            toVisit.Enqueue(c);
                    }
                }
                
            }
        }
    }

    /**
     * reset the cells in a greater area than a ghost's cost area so that cells the ghost leaves are reset
     */
    public void resetGhostAreaCost()
    {
        Cell ghostCell = mapGrid.CellFromWorldPoint(transform.position);
        // reset cells because we leave cells too
        int x0 = Mathf.Max(ghostCell.gridX - (DANGER_RANGE + 1), 0);
        int y0 = Mathf.Max(ghostCell.gridY - (DANGER_RANGE + 1), 0);
        int x1 = Mathf.Min(ghostCell.gridX + (DANGER_RANGE + 1), mapGrid.getGridSizeX() - 1);
        int y1 = Mathf.Min(ghostCell.gridY + (DANGER_RANGE + 1), mapGrid.getGridSizeY() - 1);
        mapGrid.ResetCellCosts(x0, y0, x1, y1);
    }
}
