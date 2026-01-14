using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Grid grid = new Grid(3, 3);
        grid.SetBlocked(1, 2, true);

        int startX = 0, startY = 0;
        int goalX = 2, goalY = 2;

        int[,] distance = new int[grid.Width, grid.Height];
        int[,] parentX = new int[grid.Width, grid.Height];
        int[,] parentY = new int[grid.Width, grid.Height];

        // init
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                distance[x, y] = -1;
                parentX[x, y] = -1;
                parentY[x, y] = -1;
            }
        }

        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

        distance[startX, startY] = 0;
        queue.Enqueue((startX, startY));

        bool found = false;

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();

            if (cur.x == goalX && cur.y == goalY)
            {
                found = true;
                break;
            }

            foreach (var n in grid.GetNeighbors4(cur.x, cur.y))
            {
                if (distance[n.x, n.y] != -1) continue; // already visited

                distance[n.x, n.y] = distance[cur.x, cur.y] + 1;
                parentX[n.x, n.y] = cur.x;
                parentY[n.x, n.y] = cur.y;

                queue.Enqueue(n);
            }
        }

        if (!found)
        {
            Console.WriteLine("No path found.");
            return;
        }

        // Reconstruct path from goal back to start
        List<(int x, int y)> path = new List<(int x, int y)>();
        int px = goalX, py = goalY;

        while (!(px == startX && py == startY))
        {
            path.Add((px, py));
            int nextX = parentX[px, py];
            int nextY = parentY[px, py];

            // safety (should not happen if found == true)
            if (nextX == -1 && nextY == -1)
                break;

            px = nextX;
            py = nextY;
        }

        path.Add((startX, startY));
        path.Reverse();

        Console.WriteLine("Path:");
        foreach (var p in path)
        {
            Console.WriteLine($" -> ({p.x},{p.y})");
        }
    }
}