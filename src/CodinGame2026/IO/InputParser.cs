using CodinGame2026.Helpers;
using CodinGame2026.Models;

namespace CodinGame2026.IO;

/// <summary>
/// Parses raw stdin lines into a <see cref="GameState"/> snapshot.
/// Reads from an injected <see cref="TextReader"/> so it can be unit-tested
/// without touching the real console.
/// </summary>
public static class InputParser
{
    /// <summary>
    /// Reads one turn's worth of input from <paramref name="reader"/> and returns
    /// a fully-populated <see cref="GameState"/>.
    /// </summary>
    /// <param name="reader">The source of input lines (use <c>Console.In</c> in production).</param>
    /// <param name="turn">The current 1-based turn counter.</param>
    public static GameState Parse(TextReader reader, int turn)
    {
        // TODO: implement full parsing once the game rules are published.
        // The skeleton below shows the expected pattern:

        // Example: first line might be "width height"
        // var dims   = ReadInts(reader);
        // var width  = dims[0];
        // var height = dims[1];
        // var grid   = new Grid(width, height);
        // for (int y = 0; y < height; y++)
        // {
        //     var row = reader.ReadLine()!;
        //     for (int x = 0; x < width; x++)
        //         grid[new Vec2(x, y)] = row[x];
        // }

        // Stub: return a minimal 1×1 grid with no robots
        var grid   = new Grid(1, 1);
        var robots = new List<Robot>();
        return new GameState(turn, grid, robots);
    }

    // ── Helpers ─────────────────────────────────────────────────────────────

    /// <summary>Reads one line and splits it into integers.</summary>
    public static int[] ReadInts(TextReader reader) =>
        (reader.ReadLine() ?? string.Empty)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

    /// <summary>Reads one line and splits it into strings.</summary>
    public static string[] ReadTokens(TextReader reader) =>
        (reader.ReadLine() ?? string.Empty)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
}
