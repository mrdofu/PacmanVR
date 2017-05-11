using System;
using System.Collections.Generic;

/**
 * Data structure where lower priority numeric values signify a higher priority
 * @T must adhere to the lower value, higher priority rule
 */
public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> data;

    public int Count { get { return data.Count; } }

    public PriorityQueue()
    {
        data = new List<T>();
    }

    /**
     * Returns the highest priority element in queue
     */
    public T Peek()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("Cannot call Peek on an empty queue");
        }

        return data[0];
    }

    /**
     * Adds item T into the correct position in the queue
     */
    public void Add(T item)
    {
        // binary min heap implementation
        data.Add(item);
        int i = data.Count - 1;
        while (i > 0)
        {
            int pi = (i - 1) / 2;
            if (data[i].CompareTo(data[pi]) >= 0)
            {
                break;
            }
            T tmp = data[i];
            data[i] = data[pi];
            data[pi] = tmp;
            i = pi;
        }
    }

    /**
     * Removes the item with highest priority and returns it, updating the queue.
     * Returns the removed item with highest priority.
     */
    public T Remove()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("Cannot call Remove on an empty queue");
        }

        // replacing root with last leaf simplifies structure preservation
        int li = data.Count - 1;
        T firstItem = data[0];
        data[0] = data[li];
        data.RemoveAt(li);

        li--;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li) break;
            int rc = ci + 1;
            if (rc <= li && data[rc].CompareTo(data[ci]) < 0)
                ci = rc;
            if (data[pi].CompareTo(data[ci]) <= 0) break;
            T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
            pi = ci;
        }
        return firstItem;
    }
}
