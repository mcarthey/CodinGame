using CodinGame2026.AI;
using CodinGame2026.Helpers;
using Xunit;

namespace CodinGame2026.Tests;

public class PathfinderTests
{
    // ── Helpers ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Builds a simple walkability predicate from an ASCII map.
    /// '.' = walkable, '#' = wall. Origin (0,0) is top-left.
    /// </summary>
    private static Func<Vec2, bool> MapPredicate(string[] rows)
    {
        return pos =>
            pos.Y >= 0 && pos.Y < rows.Length &&
            pos.X >= 0 && pos.X < rows[pos.Y].Length &&
            rows[pos.Y][pos.X] != '#';
    }

    // ── A* Tests ────────────────────────────────────────────────────────────

    [Fact]
    public void AStar_OpenPath_ReturnsShortestPath()
    {
        // Arrange: 1-row corridor . . . . .
        var map = new[] { "....." };
        var start = new Vec2(0, 0);
        var goal  = new Vec2(4, 0);

        // Act
        var path = Pathfinder.AStar(start, goal, MapPredicate(map));

        // Assert
        Assert.NotEmpty(path);
        Assert.Equal(start, path.First());
        Assert.Equal(goal,  path.Last());
        Assert.Equal(5, path.Count); // start + 4 steps
    }

    [Fact]
    public void AStar_PathAroundObstacle_FindsDetour()
    {
        // Arrange:
        // . # .
        // . # .
        // . . .
        var map = new[]
        {
            ".#.",
            ".#.",
            "...",
        };
        var start = new Vec2(0, 0);
        var goal  = new Vec2(2, 0);

        // Act
        var path = Pathfinder.AStar(start, goal, MapPredicate(map));

        // Assert
        Assert.NotEmpty(path);
        Assert.Equal(start, path.First());
        Assert.Equal(goal,  path.Last());
        // Path must avoid column 1 in rows 0 and 1
        Assert.DoesNotContain(new Vec2(1, 0), path);
        Assert.DoesNotContain(new Vec2(1, 1), path);
    }

    [Fact]
    public void AStar_NoPath_ReturnsEmpty()
    {
        // Arrange: goal is completely walled off
        var map = new[]
        {
            "..#.",
            "..#.",
            "..#.",
        };
        var start = new Vec2(0, 0);
        var goal  = new Vec2(3, 0);

        // Act
        var path = Pathfinder.AStar(start, goal, MapPredicate(map));

        // Assert
        Assert.Empty(path);
    }

    [Fact]
    public void AStar_SingleCell_StartEqualsGoal_ReturnsSingleElement()
    {
        var map   = new[] { "." };
        var start = new Vec2(0, 0);

        var path = Pathfinder.AStar(start, start, MapPredicate(map));

        Assert.Single(path);
        Assert.Equal(start, path[0]);
    }

    // ── BFS Tests ───────────────────────────────────────────────────────────

    [Fact]
    public void BFS_OpenPath_ReturnsShortestPath()
    {
        var map   = new[] { "....." };
        var start = new Vec2(0, 0);
        var goal  = new Vec2(4, 0);

        var path = Pathfinder.BFS(start, goal, MapPredicate(map));

        Assert.NotEmpty(path);
        Assert.Equal(start, path.First());
        Assert.Equal(goal,  path.Last());
        Assert.Equal(5, path.Count);
    }

    [Fact]
    public void BFS_PathAroundObstacle_FindsDetour()
    {
        var map = new[]
        {
            ".#.",
            ".#.",
            "...",
        };
        var start = new Vec2(0, 0);
        var goal  = new Vec2(2, 0);

        var path = Pathfinder.BFS(start, goal, MapPredicate(map));

        Assert.NotEmpty(path);
        Assert.Equal(start, path.First());
        Assert.Equal(goal,  path.Last());
        Assert.DoesNotContain(new Vec2(1, 0), path);
    }

    [Fact]
    public void BFS_NoPath_ReturnsEmpty()
    {
        var map = new[]
        {
            "..#.",
            "..#.",
            "..#.",
        };
        var path = Pathfinder.BFS(new Vec2(0, 0), new Vec2(3, 0), MapPredicate(map));

        Assert.Empty(path);
    }

    [Fact]
    public void BFS_SingleCell_StartEqualsGoal_ReturnsSingleElement()
    {
        var map   = new[] { "." };
        var start = new Vec2(0, 0);

        var path = Pathfinder.BFS(start, start, MapPredicate(map));

        Assert.Single(path);
        Assert.Equal(start, path[0]);
    }
}
