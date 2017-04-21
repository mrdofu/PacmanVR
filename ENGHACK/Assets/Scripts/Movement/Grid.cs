using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    LayerMask unwalkableMask;
    [SerializeField]
    Vector2 gridWorldSize;

    const float NODE_WIDTH = 27.8f / 17;   // must match up with gridWorldSize values in-editor
    public const float NODE_RADIUS = NODE_WIDTH / 2;

    Node[,] grid;
    int gridSizeX, gridSizeY;
    Vector3 correctedMapCenter; 

    void Start()
    {
        correctedMapCenter = transform.position;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / NODE_WIDTH);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / NODE_WIDTH);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        // origin to measure grid cells from
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // center of each grid cell in world space
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NODE_WIDTH + NODE_RADIUS) + Vector3.forward * (y * NODE_WIDTH + NODE_RADIUS);
                // smaller check radius to be forgiving on dimensions
                Vector3 halfExts = new Vector3(NODE_RADIUS - 0.1f, 2, NODE_RADIUS - 0.1f);
                // checkbox is slightly higher than center of cell to collide with higher walls too
                bool walkable = !(Physics.CheckBox(worldPoint + Vector3.up, halfExts, Quaternion.identity, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
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
     * helps to visualize grid in editor
     */
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (NODE_WIDTH - .1f));
            }
        }
    }
}

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;

    public Node(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }
}