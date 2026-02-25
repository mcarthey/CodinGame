using CodinGame2026.Helpers;

namespace CodinGame2026.Models;

/// <summary>
/// Represents a single robot entity on the grid.
/// Properties will be expanded once the game rules are published.
/// </summary>
public class Robot
{
    /// <summary>Unique identifier for this robot (as provided by CodinGame).</summary>
    public int Id { get; init; }

    /// <summary>Current grid position.</summary>
    public Vec2 Position { get; init; }

    // TODO: add game-specific fields (e.g. cargo, charge, target) once rules are known

    public Robot(int id, Vec2 position)
    {
        Id       = id;
        Position = position;
    }

    public override string ToString() => $"Robot({Id} @ {Position})";
}
