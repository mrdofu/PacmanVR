using UnityEngine;
using System;

/**
 * Grid cell representation of map for navigation
 */
public class Cell: IComparable<Cell>
{
    public bool walkable { get; private set; }
    public Vector3 worldPosition { get; private set; }
    public int gridX { get; private set; }
    public int gridY { get; private set; }

    private int _cost = 0;
    public int cost {
        get
        {
            return _cost;
        }
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException("Cost must be greater than or equal to 0.");
            _cost = value;
        }
    }

    public Cell(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int CompareTo(Cell other)
    {
        int comp = cost - other.cost;
        if (comp < 0) comp = -1;
        else if (comp > 0) comp = 1;

        return comp;
    }
}