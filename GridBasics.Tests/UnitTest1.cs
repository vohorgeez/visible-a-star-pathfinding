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
}