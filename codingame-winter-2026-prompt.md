# CodinGame Winter Challenge 2026 (Exotec) — Bootstrap Prompt for Claude Code

## Context

I am entering the **CodinGame Winter Challenge 2026**, sponsored by Exotec (French industrial robotics/warehouse automation company). The contest runs for two weeks starting **March 10, 2026**. The game theme is robotics warehouse logistics — likely involving grid-based robot routing, pathfinding, collision avoidance, and/or task scheduling.

The game rules are not yet published — they will be revealed on contest start day. This prompt is for scaffolding the **project structure, tooling, and architecture** so I can hit the ground running the moment the rules drop.

## My Background

- Senior .NET developer and architect, 30+ years experience
- Language of choice: **C#**
- IDE: Visual Studio or VS Code
- I prefer clean architecture, SOLID principles, and test-driven development
- I want to develop locally in a proper multi-file solution and flatten to a single `Program.cs` for CodinGame submission

---

## What I Need You to Build

### 1. Solution Structure

Create a .NET solution with the following projects:

```
CodinGame2026/
├── CodinGame2026.sln
├── src/
│   └── CodinGame2026/           # Main bot project (net8.0 console app)
│       ├── Program.cs           # Entry point: stdin loop, game orchestration
│       ├── IO/
│       │   └── InputParser.cs   # Parses raw stdin lines into GameState
│       ├── Models/
│       │   ├── GameState.cs     # Full game state snapshot each turn
│       │   ├── Grid.cs          # Grid representation (cells, dimensions)
│       │   └── Robot.cs         # Robot entity (position, state, etc.)
│       ├── AI/
│       │   ├── IStrategy.cs     # Strategy interface
│       │   ├── BotStrategy.cs   # Main strategy implementation
│       │   └── Pathfinder.cs    # A* / BFS pathfinding on grid
│       └── Helpers/
│           └── Vec2.cs          # 2D vector/point struct for grid coords
├── tests/
│   └── CodinGame2026.Tests/     # xUnit test project
│       ├── PathfinderTests.cs
│       ├── InputParserTests.cs
│       └── BotStrategyTests.cs
└── tools/
    └── merge.ps1                # PowerShell script to flatten to single Program.cs
```

### 2. Core Architecture Principles

- **IO is isolated**: `InputParser` reads from a `TextReader` (not directly from `Console.In`) so it can be injected in tests
- **Strategy is pure**: `BotStrategy.GetAction(GameState state)` takes a state object and returns a command string — no side effects, no IO
- **GameState is immutable-friendly**: constructed fresh each turn from parsed input
- **Pathfinder is generic**: operates on `Grid` with a cost/walkability function — not hardcoded to any specific game mechanic
- **Program.cs** is thin: just the game loop, reading input, calling strategy, writing output

### 3. Program.cs Game Loop Pattern

```csharp
// CodinGame standard pattern
while (true)
{
    var state = InputParser.Parse(Console.In);
    var action = strategy.GetAction(state);
    Console.WriteLine(action);
}
```

### 4. Pathfinder Requirements

Implement A* with the following characteristics:
- Operates on a 2D grid of `Vec2` coordinates
- Accepts a `Func<Vec2, bool> isWalkable` predicate (so it works regardless of game-specific obstacle types)
- Accepts a `Func<Vec2, Vec2, int> moveCost` function (defaults to 1 per step)
- Returns `List<Vec2>` representing the path (empty if no path found)
- Include BFS as an alternative (sometimes better for unweighted grids)

### 5. Vec2 Struct

```csharp
public readonly struct Vec2
{
    public int X { get; }
    public int Y { get; }
    // Constructor, equality, operators (+, -, ==, !=)
    // Static directions: Up, Down, Left, Right, and All4 / All8 collections
    // ManhattanDistance(Vec2 other)
    // ToString() → "X Y" (CodinGame output format)
}
```

### 6. Test Project

Wire up an **xUnit** test project that references the src project. Scaffold the following test classes with placeholder tests:

- `PathfinderTests` — tests for A* including: open path, path around obstacle, no path exists, single cell
- `InputParserTests` — tests that parse mock stdin strings into expected `GameState` objects
- `BotStrategyTests` — tests that given a known `GameState`, the strategy returns an expected action string

Tests should use `TextReader` injection so no real stdin is needed.

### 7. Merge Script (`tools/merge.ps1`)

Create a PowerShell script that:
- Collects all `.cs` files from the `src/CodinGame2026/` project
- Strips duplicate `using` statements
- Strips `namespace` wrappers (or handles file-scoped namespaces)
- Concatenates everything into a single `Program.cs` suitable for CodinGame submission
- Outputs to `tools/output/Program.cs`

### 8. Placeholder / Stub Behavior

Since the game rules aren't known yet:
- `GameState` should have a few obvious placeholder properties (turn number, a grid, a list of robots) that are easy to expand
- `InputParser.Parse()` should read a configurable number of lines and return a stub state — add a `// TODO: implement parsing` comment
- `BotStrategy.GetAction()` should return `"WAIT"` by default with a `// TODO: implement strategy` comment
- The project should **compile and run** cleanly in this stub state

---

## Constraints & Notes

- Target **.NET 8** (LTS, good CodinGame support)
- Use **xUnit** for testing
- No external NuGet packages in the main bot project (CodinGame doesn't support them) — test project can use xUnit packages freely
- Keep `Program.cs` as the single entry point; CodinGame reads from the top-level class
- Add XML doc comments to public interfaces and key classes
- The merge script is for convenience — I'll copy-paste the output into CodinGame's editor

---

## Deliverable

Build the full solution as described. Make sure `dotnet build` and `dotnet test` both pass cleanly from the solution root before finishing.
