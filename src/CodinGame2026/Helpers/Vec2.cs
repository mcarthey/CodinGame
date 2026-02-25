namespace CodinGame2026.Helpers;

/// <summary>
/// Immutable 2D integer vector / grid coordinate. Used throughout the bot for
/// all position and direction arithmetic.
/// </summary>
public readonly struct Vec2 : IEquatable<Vec2>
{
    public int X { get; }
    public int Y { get; }

    public Vec2(int x, int y)
    {
        X = x;
        Y = y;
    }

    // ── Cardinal directions ─────────────────────────────────────────────────

    public static readonly Vec2 Up    = new(0, -1);
    public static readonly Vec2 Down  = new(0,  1);
    public static readonly Vec2 Left  = new(-1, 0);
    public static readonly Vec2 Right = new( 1, 0);

    /// <summary>The four cardinal neighbours (N, S, W, E).</summary>
    public static readonly IReadOnlyList<Vec2> All4 = [Up, Down, Left, Right];

    /// <summary>All eight neighbours including diagonals.</summary>
    public static readonly IReadOnlyList<Vec2> All8 =
    [
        new(-1, -1), Up,    new(1, -1),
        Left,                Right,
        new(-1,  1), Down,  new(1,  1),
    ];

    // ── Arithmetic operators ────────────────────────────────────────────────

    public static Vec2 operator +(Vec2 a, Vec2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vec2 operator -(Vec2 a, Vec2 b) => new(a.X - b.X, a.Y - b.Y);
    public static Vec2 operator *(Vec2 v, int s)  => new(v.X * s,   v.Y * s);
    public static Vec2 operator *(int s, Vec2 v)  => v * s;

    // ── Equality ────────────────────────────────────────────────────────────

    public static bool operator ==(Vec2 a, Vec2 b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Vec2 a, Vec2 b) => !(a == b);
    public bool Equals(Vec2 other) => this == other;
    public override bool Equals(object? obj) => obj is Vec2 v && this == v;
    public override int GetHashCode() => HashCode.Combine(X, Y);

    // ── Utility ─────────────────────────────────────────────────────────────

    /// <summary>Manhattan distance to <paramref name="other"/>.</summary>
    public int ManhattanDistance(Vec2 other) =>
        Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    /// <summary>Returns "X Y" — the standard CodinGame output format for a coordinate.</summary>
    public override string ToString() => $"{X} {Y}";
}
