using CodinGame2026.Helpers;

namespace CodinGame2026.Models;

/// <summary>
/// Represents the game grid. Cells are addressed by (X = column, Y = row).
/// </summary>
public class Grid
{
    public int Width  { get; }
    public int Height { get; }

    // TODO: replace with game-specific cell type once rules are known
    private readonly char[,] _cells;

    public Grid(int width, int height)
    {
        Width  = width;
        Height = height;
        _cells = new char[width, height];
    }

    /// <summary>Returns true when <paramref name="pos"/> is within grid bounds.</summary>
    public bool InBounds(Vec2 pos) =>
        pos.X >= 0 && pos.X < Width &&
        pos.Y >= 0 && pos.Y < Height;

    /// <summary>Gets or sets the raw cell character at <paramref name="pos"/>.</summary>
    public char this[Vec2 pos]
    {
        get => _cells[pos.X, pos.Y];
        set => _cells[pos.X, pos.Y] = value;
    }

    /// <summary>
    /// Default walkability predicate: a cell is walkable when it is not a wall ('#').
    /// Override by passing a custom predicate to the pathfinder.
    /// </summary>
    public bool IsWalkable(Vec2 pos) =>
        InBounds(pos) && _cells[pos.X, pos.Y] != '#';

    /// <summary>Enumerates all in-bounds neighbours of <paramref name="pos"/> using the four cardinal directions.</summary>
    public IEnumerable<Vec2> Neighbours(Vec2 pos)
    {
        foreach (var dir in Vec2.All4)
        {
            var nb = pos + dir;
            if (InBounds(nb)) yield return nb;
        }
    }
}
