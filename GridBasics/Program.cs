using System;

class Program
{
    static void Main()
    {
        Grid grid = new Grid(3,3);
        grid.SetBlocked(1, 2, true);

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                string cellState = grid.IsBlocked(x, y) ? "X" : ".";
                Console.WriteLine($"Cell ({x},{y}) = {cellState}");
            }
        }

        Console.WriteLine("Neighbors of (0,0):");

        foreach (var n in grid.GetNeighbors4(0, 0))
        {
            Console.WriteLine($" -> ({n.x},{n.y})");
        }
    }
}