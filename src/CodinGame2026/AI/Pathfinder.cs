using CodinGame2026.Helpers;

namespace CodinGame2026.AI;

/// <summary>
/// Generic grid pathfinder. Provides both A* (weighted) and BFS (unweighted)
/// implementations. Neither depends on game-specific types — callers supply
/// walkability and cost predicates.
/// </summary>
public static class Pathfinder
{
    // ── A* ──────────────────────────────────────────────────────────────────

    /// <summary>
    /// Finds the lowest-cost path from <paramref name="start"/> to <paramref name="goal"/>
    /// using the A* algorithm with a Manhattan-distance heuristic.
    /// </summary>
    /// <param name="start">Starting cell.</param>
    /// <param name="goal">Target cell.</param>
    /// <param name="isWalkable">Returns <c>true</c> when a cell may be entered.</param>
    /// <param name="moveCost">
    /// Edge cost from one cell to an adjacent cell. Defaults to 1 per step when
    /// <c>null</c>.
    /// </param>
    /// <returns>
    /// Ordered list of cells from <paramref name="start"/> (inclusive) to
    /// <paramref name="goal"/> (inclusive), or an empty list when no path exists.
    /// </returns>
    public static List<Vec2> AStar(
        Vec2 start,
        Vec2 goal,
        Func<Vec2, bool> isWalkable,
        Func<Vec2, Vec2, int>? moveCost = null)
    {
        moveCost ??= static (_, _) => 1;

        if (!isWalkable(start) || !isWalkable(goal))
            return [];

        // g-score: best known cost from start to a node
        var gScore = new Dictionary<Vec2, int> { [start] = 0 };
        // came-from map for path reconstruction
        var cameFrom = new Dictionary<Vec2, Vec2>();
        // open set — (fScore, node)
        var open = new PriorityQueue<Vec2, int>();
        open.Enqueue(start, Heuristic(start, goal));

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current == goal)
                return ReconstructPath(cameFrom, current);

            foreach (var neighbour in CardinalNeighbours(current))
            {
                if (!isWalkable(neighbour)) continue;

                int tentativeG = gScore[current] + moveCost(current, neighbour);

                if (!gScore.TryGetValue(neighbour, out int knownG) || tentativeG < knownG)
                {
                    gScore[neighbour]   = tentativeG;
                    cameFrom[neighbour] = current;
                    int fScore = tentativeG + Heuristic(neighbour, goal);
                    open.Enqueue(neighbour, fScore);
                }
            }
        }

        return []; // no path found
    }

    // ── BFS ─────────────────────────────────────────────────────────────────

    /// <summary>
    /// Finds the shortest path (fewest steps) from <paramref name="start"/> to
    /// <paramref name="goal"/> using breadth-first search. Prefer this over A*
    /// when all edge costs are uniform.
    /// </summary>
    /// <param name="start">Starting cell.</param>
    /// <param name="goal">Target cell.</param>
    /// <param name="isWalkable">Returns <c>true</c> when a cell may be entered.</param>
    /// <returns>
    /// Ordered list of cells from <paramref name="start"/> (inclusive) to
    /// <paramref name="goal"/> (inclusive), or an empty list when no path exists.
    /// </returns>
    public static List<Vec2> BFS(Vec2 start, Vec2 goal, Func<Vec2, bool> isWalkable)
    {
        if (!isWalkable(start) || !isWalkable(goal))
            return [];

        var cameFrom = new Dictionary<Vec2, Vec2>();
        var queue    = new Queue<Vec2>();
        queue.Enqueue(start);
        cameFrom[start] = start;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == goal)
                return ReconstructPath(cameFrom, current, start);

            foreach (var neighbour in CardinalNeighbours(current))
            {
                if (!isWalkable(neighbour) || cameFrom.ContainsKey(neighbour)) continue;
                cameFrom[neighbour] = current;
                queue.Enqueue(neighbour);
            }
        }

        return []; // no path found
    }

    // ── Internals ───────────────────────────────────────────────────────────

    private static int Heuristic(Vec2 a, Vec2 b) => a.ManhattanDistance(b);

    private static IEnumerable<Vec2> CardinalNeighbours(Vec2 pos)
    {
        foreach (var dir in Vec2.All4)
            yield return pos + dir;
    }

    private static List<Vec2> ReconstructPath(Dictionary<Vec2, Vec2> cameFrom, Vec2 current)
    {
        var path = new List<Vec2>();
        while (cameFrom.TryGetValue(current, out var prev))
        {
            path.Add(current);
            if (prev == current) break; // reached origin sentinel
            current = prev;
        }
        path.Add(current);
        path.Reverse();
        return path;
    }

    private static List<Vec2> ReconstructPath(Dictionary<Vec2, Vec2> cameFrom, Vec2 current, Vec2 start)
    {
        var path = new List<Vec2>();
        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}
