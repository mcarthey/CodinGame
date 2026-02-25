using CodinGame2026.Models;

namespace CodinGame2026.AI;

/// <summary>
/// Main strategy implementation. Replace the stub body with real logic once
/// the game rules are published.
/// </summary>
public class BotStrategy : IStrategy
{
    /// <inheritdoc/>
    public string GetAction(GameState state)
    {
        // TODO: implement strategy once game rules are known.
        // Typical pattern:
        //   1. Analyse state (robots, grid, targets)
        //   2. For each controllable robot, compute the best move via Pathfinder
        //   3. Return one output line per robot

        return "WAIT";
    }
}
