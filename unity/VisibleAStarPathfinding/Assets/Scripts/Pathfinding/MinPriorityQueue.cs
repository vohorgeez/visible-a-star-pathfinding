using System.Collections.Generic;

public class MinPriorityQueue<T>
{
    private class Node
    {
        public T Item;
        public int Priority;

        public Node(T item, int priority)
        {
            Item = item;
            Priority = priority;
        }
    }

    private readonly List<Node> items = new List<Node>();

    public int Count
    {
        get { return items.Count; }
    }

    public void Enqueue(T item, int priority)
    {
        int i = 0;
        while (i < items.Count && items[i].Priority <= priority) i++;
        items.Insert(i, new Node(item, priority));
    }

    public T Dequeue()
    {
        Node first = items[0];
        items.RemoveAt(0);
        return first.Item;
    }
}