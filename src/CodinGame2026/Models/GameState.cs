namespace CodinGame2026.Models;

/// <summary>
/// Immutable snapshot of the full game state for a single turn.
/// Constructed fresh every turn from parsed stdin input.
/// </summary>
public class GameState
{
    /// <summary>Current turn number (1-based).</summary>
    public int Turn { get; init; }

    /// <summary>The playing grid for this turn.</summary>
    public Grid Grid { get; init; }

    /// <summary>All robots visible this turn (our bots and, if applicable, opponents).</summary>
    public IReadOnlyList<Robot> Robots { get; init; }

    // TODO: add further game-specific state (e.g. packages, score, time limit) once rules are known

    public GameState(int turn, Grid grid, IReadOnlyList<Robot> robots)
    {
        Turn   = turn;
        Grid   = grid;
        Robots = robots;
    }
}
