# CodinGame
CodinGame challenges: https://www.codingame.com/contests

---

## Winter Challenge 2026 (Exotec)

**Contest starts:** March 10, 2026 — https://www.codingame.com/contests/winter-challenge-2026-exotec

This repository contains a pre-scaffolded C# (.NET 8) bot template ready for the contest.
The game rules are not yet published; the scaffold is designed so you can fill in the blanks
the moment they drop.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PowerShell 7+](https://github.com/PowerShell/PowerShell/releases) (for the merge script)
- Visual Studio 2022 **or** VS Code with the C# Dev Kit extension

---

## Getting started

```bash
# Clone the repo
git clone https://github.com/mcarthey/CodinGame.git
cd CodinGame

# Build the solution
dotnet build CodinGame2026.sln

# Run all tests
dotnet test CodinGame2026.sln
```

Everything should build and all tests should pass against the stub implementation.

---

## Project structure

```
CodinGame2026.sln
├── src/CodinGame2026/              # Bot (no external NuGet packages — CodinGame constraint)
│   ├── Program.cs                 # Entry point: reads stdin, calls strategy, writes stdout
│   ├── IO/InputParser.cs          # Parses raw stdin into GameState
│   ├── Models/
│   │   ├── GameState.cs           # Immutable turn snapshot
│   │   ├── Grid.cs                # 2D grid with walkability helpers
│   │   └── Robot.cs               # Robot entity
│   ├── AI/
│   │   ├── IStrategy.cs           # Strategy interface
│   │   ├── BotStrategy.cs         # Main strategy (start here on contest day)
│   │   └── Pathfinder.cs          # Generic A* and BFS
│   └── Helpers/Vec2.cs            # 2D coordinate struct
├── tests/CodinGame2026.Tests/      # xUnit test project
│   ├── PathfinderTests.cs
│   ├── InputParserTests.cs
│   └── BotStrategyTests.cs
└── tools/
    └── merge.ps1                  # Flattens solution to a single Program.cs for submission
```

---

## Contest day workflow

### 1. Read the rules and update the models

Open `src/CodinGame2026/Models/` and expand the stubs to match the published game rules:

- **`GameState.cs`** — add fields for score, packages, time limits, etc.
- **`Robot.cs`** — add cargo, charge level, target cell, or whatever the game tracks.
- **`Grid.cs`** — update the cell type / walkability logic to match the actual map format.

### 2. Implement the input parser

Open `src/CodinGame2026/IO/InputParser.cs` and replace the `// TODO` stub with real parsing.
The helper methods `ReadInts` and `ReadTokens` are already wired up:

```csharp
public static GameState Parse(TextReader reader, int turn)
{
    var dims  = ReadInts(reader);          // e.g. "width height"
    var grid  = new Grid(dims[0], dims[1]);
    // ... parse robots, packages, etc.
    return new GameState(turn, grid, robots);
}
```

Because `Parse` accepts a `TextReader`, you can write unit tests against it immediately
using `StringReader` — no real console input needed.

### 3. Implement the strategy

Open `src/CodinGame2026/AI/BotStrategy.cs` and replace the `"WAIT"` stub:

```csharp
public string GetAction(GameState state)
{
    // Example: move each robot toward its nearest target
    var lines = new List<string>();
    foreach (var robot in state.Robots)
    {
        var path = Pathfinder.BFS(robot.Position, target, state.Grid.IsWalkable);
        var next = path.Count > 1 ? path[1] : robot.Position;
        lines.Add($"MOVE {next}");
    }
    return string.Join("\n", lines);
}
```

The `Pathfinder` class provides both **A\*** (weighted) and **BFS** (unweighted). Pass in any
`Func<Vec2, bool>` walkability predicate and an optional `Func<Vec2, Vec2, int>` cost function.

### 4. Test locally

```bash
dotnet test CodinGame2026.sln --logger "console;verbosity=detailed"
```

Add new test cases to `tests/CodinGame2026.Tests/` as you iterate on the strategy.

### 5. Submit to CodinGame

Run the merge script to flatten all source files into one `Program.cs`:

```powershell
pwsh tools/merge.ps1
```

The output is written to `tools/output/Program.cs`. Open that file, copy its entire
contents, and paste it into the CodinGame editor. The script:

- Collects all `.cs` files from `src/CodinGame2026/`
- De-duplicates `using` directives
- Strips file-scoped `namespace` declarations
- Appends `Program.cs` last so top-level statements remain at the end

> **Note:** `tools/output/` is git-ignored — only the source files are committed.

---

## Key design principles

| Principle | How it's enforced |
|---|---|
| IO is isolated | `InputParser.Parse(TextReader, int)` — inject `Console.In` in production, `StringReader` in tests |
| Strategy is pure | `IStrategy.GetAction(GameState)` has no IO or side-effects |
| State is immutable-friendly | `GameState` is constructed fresh each turn from parsed input |
| Pathfinder is generic | A* and BFS accept `Func<Vec2, bool>` and `Func<Vec2, Vec2, int>` — not tied to any game mechanic |
| No external packages in src | CodinGame doesn't support NuGet; only the test project references xUnit |
