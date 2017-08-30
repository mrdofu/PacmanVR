using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    [SerializeField]
    LayerMask dotsMask;
    [SerializeField]
    LayerMask unwalkableMask;
    [SerializeField]
    Vector2 gridWorldSize;

    const float CELL_WIDTH = 27.8f / 17;   // must match up with gridWorldSize values in-editor
    public const float CELL_RADIUS = CELL_WIDTH / 2;
    const float FORGIVENESS_TERM = 0.1f;

    private Cell[,] grid;
    private int gridSizeX, gridSizeY;
    private Vector3 correctedMapCenter; 

    void Start()
    {
        correctedMapCenter = transform.position;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / CELL_WIDTH);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / CELL_WIDTH);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Cell[gridSizeX, gridSizeY];
        // origin to measure grid cells from
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // center of each grid cell in world space
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * CELL_WIDTH + CELL_RADIUS) + Vector3.forward * (y * CELL_WIDTH + CELL_RADIUS);
                // smaller check radius to be forgiving on dimensions
                Vector3 halfExts = new Vector3(CELL_RADIUS - FORGIVENESS_TERM, 2, CELL_RADIUS - FORGIVENESS_TERM);
                // checkbox is slightly higher than center of cell to collide with higher walls too
                bool walkable = !(Physics.CheckBox(worldPoint + Vector3.up, halfExts, Quaternion.identity, unwalkableMask));
                grid[x, y] = new Cell(walkable, worldPoint, x, y);
            }
        }
    }

    /**
     * returns grid cell given a world position
     */
    public Cell CellFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    /**
     * returns true if the grid cell has a dot in it
     */
     public bool CellHasDot(Cell c)
    {
        return Physics.CheckSphere(c.worldPosition, CELL_RADIUS - FORGIVENESS_TERM, 
            dotsMask, QueryTriggerInteraction.Collide);
    }

    /**
     * returns a given cell's neighbours considering edges and unwalkable areas
     */
    public List<Cell> GetCellNeighbours(Cell c)
    {
        List<Cell> neighbs = new List<Cell>();

        // check for x edges
        for (int x = -1; x <= 1; x += 2)
        {
            int checkX = c.gridX + x;
            if (checkX >= 0 && checkX < gridSizeX && grid[checkX, c.gridY].walkable)
            {
                neighbs.Add(grid[checkX, c.gridY]);
            }
        }
        // check for y edges
        for (int y = -1; y <= 1; y += 2)
        {
            int checkY = c.gridY + y;
            if (checkY >= 0 && checkY < gridSizeY && grid[c.gridX, checkY].walkable)
            {
                neighbs.Add(grid[c.gridX, checkY]);
            }
        }

        return neighbs;
    }

    /**
     * Resets cell costs in a rectangle from point (x0, y0) to point (x1, y1) inclusive.
     * @throws IndexOutOfRangeException if any point coordinate is out of range
     */
    public void ResetCellCosts(int x0, int y0, int x1, int y1)
    {
        for (int x = x0; x <= x1; x++)
        {
            for (int y = y0; y <= y1; y++)
            {
                grid[x, y].cost = 0;
            }
        }
    }

    /**
     * helps to visualize grid in editor
     */
    void OnDrawGizmosSelected()
    {
        // whole grid outline
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        // individual cells
        if (grid != null)
        {
            foreach (Cell c in grid)
            {
                Gizmos.color = (c.walkable) ? Color.white : Color.red;
                if (c.cost > 0)
                {
                    Color costBlue = Color.blue;
                    costBlue.a = (float) c.cost / 10;
                    Gizmos.color = costBlue;
                }
                else if (CellHasDot(c))
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawWireCube(c.worldPosition, Vector3.one * (CELL_WIDTH - FORGIVENESS_TERM));
                //Gizmos.DrawWireSphere(c.worldPosition, 1);
            }
        }
    }

    /* GETTERS AND SETTERS */
    public int getGridSizeX()
    {
        return gridSizeX;
    }

    public int getGridSizeY()
    {
        return gridSizeY;
    }
}
