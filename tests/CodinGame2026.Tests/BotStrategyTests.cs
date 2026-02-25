using CodinGame2026.AI;
using CodinGame2026.Models;
using Xunit;

namespace CodinGame2026.Tests;

public class BotStrategyTests
{
    // ── Helpers ─────────────────────────────────────────────────────────────

    private static GameState MakeState(int turn = 1) =>
        new(turn, new Grid(5, 5), []);

    private readonly IStrategy _strategy = new BotStrategy();

    // ── Tests ────────────────────────────────────────────────────────────────

    [Fact]
    public void GetAction_StubState_ReturnsWait()
    {
        // The default stub strategy always returns WAIT until implemented.
        var state  = MakeState();
        var action = _strategy.GetAction(state);

        Assert.Equal("WAIT", action);
    }

    [Fact]
    public void GetAction_ReturnsNonNullNonEmptyString()
    {
        var state  = MakeState(turn: 5);
        var action = _strategy.GetAction(state);

        Assert.False(string.IsNullOrWhiteSpace(action));
    }

    [Fact]
    public void GetAction_IsDeterministic_SameInputSameOutput()
    {
        // Strategy must be pure — same state must always produce same action.
        var state   = MakeState(turn: 10);
        var action1 = _strategy.GetAction(state);
        var action2 = _strategy.GetAction(state);

        Assert.Equal(action1, action2);
    }

    [Fact]
    public void GetAction_DoesNotThrow_OnEmptyGrid()
    {
        var state      = new GameState(1, new Grid(1, 1), []);
        var exception  = Record.Exception(() => _strategy.GetAction(state));

        Assert.Null(exception);
    }
}
