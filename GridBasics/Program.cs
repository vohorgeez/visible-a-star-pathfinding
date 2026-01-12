using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Grid grid = new Grid(3, 3);
        grid.SetBlocked(1, 2, true);

        int[,] distance = new int[grid.Width, grid.Height];

        // Initialisation: -1 = non visité
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                distance[x, y] = -1;
            }
        }

        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

        // Point de départ
        int startX = 0;
        int startY = 0;

        distance[startX, startY] = 0;
        queue.Enqueue((startX, startY));

        // BFS
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int cx = current.x;
            int cy = current.y;

            foreach (var n in grid.GetNeighbors4(cx, cy))
            {
                if (distance[n.x, n.y] == -1)
                {
                    distance[n.x, n.y] = distance[cx, cy] + 1;
                    queue.Enqueue(n);
                }
            }
        }

        // Affichage des distances
        Console.WriteLine("Distances:");
        for (int y = grid.Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (distance[x, y] == -1)
                    Console.Write("X");
                else
                    Console.Write($"{distance[x, y]}");
            }
            Console.WriteLine();
        }
    }
}