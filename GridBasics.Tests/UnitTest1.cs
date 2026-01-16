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
}