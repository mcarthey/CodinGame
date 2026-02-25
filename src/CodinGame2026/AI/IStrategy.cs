using CodinGame2026.Models;

namespace CodinGame2026.AI;

/// <summary>
/// Contract for all bot strategies. Implementations must be pure: given the
/// same <see cref="GameState"/> they must produce the same output with no
/// observable side-effects (no IO, no global mutation).
/// </summary>
public interface IStrategy
{
    /// <summary>
    /// Decides the action(s) to take for this turn.
    /// </summary>
    /// <param name="state">Full game state snapshot for the current turn.</param>
    /// <returns>
    /// A string to be written verbatim to stdout — e.g. <c>"MOVE 3 2"</c> or a
    /// multi-line block of commands, one per robot.
    /// </returns>
    string GetAction(GameState state);
}
