using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder
{
    public List<Vector2Int> FindPath(Grid grid, Vector2Int start, Vector2Int goal)
    {
        int INF = int.MaxValue / 4;

        int[,] g = new int[grid.Width, grid.Height];
        int[,] parentX = new int[grid.Width, grid.Height];
        int[,] parentY = new int[grid.Width, grid.Height];

        for (int x = 0; x < grid.Width; x++)
            for (int y = 0; y < grid.Height; y++)
            {
                g[x, y] = INF;
                parentX[x, y] = -1;
                parentY[x, y] = -1;
            }

        var open = new MinPriorityQueue<Vector2Int>();

        g[start.x, start.y] = 0;
        open.Enqueue(start, HeuristicManhattan(start, goal));

        while (open.Count > 0)
        {
            var cur = open.Dequeue();

            if (cur == goal)
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

                    int f = candidate + HeuristicManhattan(n, goal);
                    open.Enqueue(n, f);
                }
            }
        }
        return new List<Vector2Int>(); // pas de chemin
    }

    private static int HeuristicManhattan(Vector2Int a, Vector2Int b)
        => Math.Abs(b.x - a.x) + Math.Abs(b.y - a.y);

    private static List<Vector2Int> ReconstructPath(
        int[,] parentX, int[,] parentY,
        Vector2Int start, Vector2Int goal)
    {
        var path = new List<Vector2Int>();
        int px = goal.x, py = goal.y;

        // unreachable safety
        if (goal != start && parentX[px, py] == -1 && parentY[px, py] == -1)
            return path;

        while (!(px == start.x && py == start.y))
        {
            path.Add(new Vector2Int(px, py));
            int nx = parentX[px, py];
            int ny = parentY[px, py];
            px = nx; py = ny;
        }

        path.Add(start);
        path.Reverse();
        return path;
    }
}