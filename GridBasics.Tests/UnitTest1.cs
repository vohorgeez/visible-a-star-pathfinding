using NUnit.Framework;

public class AStarPathfinderTests
{
    [Test]
    public void FindPath_NoObstacles_ReturnsNonEmptyPath()
    {
        // Arrange
        var grid = new Grid(3, 3);
        var pathfinder = new AStarPathfinder();

        // Act
        var path = pathfinder.FindPath(grid, (0, 0), (2, 2));

        // Assert
        Assert.That(path, Is.Not.Null);
        Assert.That(path!.Count, Is.GreaterThan(0));

        // Bonus sanity: start & goal match
        Assert.That(path[0], Is.EqualTo((0, 0)));
        Assert.That(path[^1], Is.EqualTo((2, 2)));
    }

    [Test]
    public void FindPath_EmptyGrid_ReturnsShortestPath()
    {
        // Arrange
        var grid = new Grid(3, 3);
        var pathfinder = new AStarPathfinder();

        // Act
        var path = pathfinder.FindPath(grid, (0, 0), (2, 2));

        // Assert
        Assert.That(path, Is.Not.Null);

        // 4 moves => 5 nodes
        Assert.That(path!.Count, Is.EqualTo(5),
            "Path should have minimal length on empty grid.");
    }

    [Test]
    public void FindPath_BlockedStartNeighbors_ReturnsNull()
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.SetBlocked(1, 0, true);
        grid.SetBlocked(0, 1, true);

        var pathfinder = new AStarPathfinder();

        // Act
        var path = pathfinder.FindPath(grid, (0, 0), (2, 2));

        // Assert
        Assert.That(path, Is.Null, "Path should be null when start is trapped.");
    }

    [Test]
    public void FindPath_AvoidsExpensiveTerrain()
    {
        // Arrange
        var grid = new Grid(3, 3);
        grid.SetCost(1, 1, 10); // terrain cher au centre

        var pathfinder = new AStarPathfinder();

        // Act
        var path = pathfinder.FindPath(grid, (0, 0), (2, 2));

        // Assert
        Assert.That(path, Is.Not.Null);

        // Le chemin ne doit PAS passer par la case chère
        Assert.That(path, Does.Not.Contain((1, 1)),
            "A* should avoid expensive terrain even if path is longer or equal in steps.");
    }

    private static int PathCost(Grid grid, System.Collections.Generic.List<(int x, int y)> path)
    {
        // On "paie" le coût d'entrée sur chaque case après le start
        int total = 0;
        for (int i = 1; i < path.Count; i++)
        {
            var p = path[i];
            total += grid.GetCost(p.x, p.y);
        }
        return total;
    }

    [Test]
    public void AStar_And_Dijkstra_ReturnSameOptimalCost()
    {
        // Arrange
        var grid = new Grid(5, 5);

        // Quelques murs
        grid.SetBlocked(1, 2, true);
        grid.SetBlocked(3, 1, true);
        grid.SetBlocked(2, 4, true);

        // Quelques coûts
        grid.SetCost(2, 2, 5);
        grid.SetCost(2, 3, 5);
        grid.SetCost(4, 3, 3);

        var start = (0, 0);
        var goal = (4, 4);

        var aStar = new AStarPathfinder();
        var dijkstra = new DijkstraPathfinder();

        // Act
        var pathA = aStar.FindPath(grid, start, goal);
        var pathD = dijkstra.FindPath(grid, start, goal);

        // Assert: existence cohérente
        Assert.That(pathA, Is.Not.Null);
        Assert.That(pathD, Is.Not.Null);

        int costA = PathCost(grid, pathA!);
        int costD = PathCost(grid, pathD!);

        Assert.That(costA, Is.EqualTo(costD),
            "A* must match Dijkstra's optimal cost (admissible heuristic).");
    }
}