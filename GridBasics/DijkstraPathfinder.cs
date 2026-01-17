using System;
using System.Collections.Generic;

public class DijkstraPathfinder
{
    public List<(int x, int y)>? FindPath(Grid grid, (int x, int y) start, (int x, int y) goal)
    {
        int INF = int.MaxValue / 4;

        int[,] g = new int[grid.Width, grid.Height];
        int[,] parentX = new int[grid.Width, grid.Height];
        int[,] parentY = new int[grid.Width, grid.Height];

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                g[x, y] = INF;
                parentX[x, y] = -1;
                parentY[x, y] = -1;
            }
        }

        var pq = new PriorityQueue<(int x, int y), int>();

        g[start.x, start.y] = 0;
        pq.Enqueue(start, 0);

        while (pq.Count > 0)
        {
            var cur = pq.Dequeue();

            if (cur.x == goal.x && cur.y == goal.y)
                return ReconstructPath(parentX, parentY, start, goal);
            
            int curCost = g[cur.x, cur.y];

            foreach (var n in grid.GetNeighbors4(cur.x, cur.y))
            {
                int step = grid.GetCost(n.x, n.y);
                int candidate = curCost + step;

                if (candidate < g[n.x, n.y])
                {
                    g[n.x, n.y] = candidate;
                    parentX[n.x, n.y] = cur.x;
                    parentY[n.x, n.y] = cur.y;

                    pq.Enqueue(n, candidate);
                }
            }
        }

        return null;
    }

    private static List<(int x, int y)> ReconstructPath(
        int[,] parentX, int[,] parentY,
        (int x, int y) start, (int x, int y) goal)
    {
        var path = new List<(int x, int y)>();
        int px = goal.x, py = goal.y;

        if (!(px == start.x && py == start.y) && parentX[px, py] == -1 && parentY[px, py] == -1)
            return path;
        
        while (!(px == start.x && py == start.y))
        {
            path.Add((px, py));
            int nx = parentX[px, py];
            int ny = parentY[px, py];
            px = nx; py = ny;
        }

        path.Add(start);
        path.Reverse();
        return path;
    }
}