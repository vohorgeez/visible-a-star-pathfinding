using System;

class Program
{
    static void Main()
    {
        Grid grid = new Grid(5, 5);

        grid.SetBlocked(1, 2, true);
        grid.SetBlocked(3, 1, true);

        grid.SetCost(2, 2, 5);
        grid.SetCost(2, 3, 5);

        var pathfinder = new AStarPathFinder();
        var path = pathfinder.FindPath(grid, (0, 0), (4, 4));

        if (path == null || path.Count == 0)
        {
            Console.WriteLine("No path found.");
            return;
        }

        Console.WriteLine("Path:");
        foreach (var p in path)
            Console.WriteLine($" -> ({p.x},{p.y})");
    }
}