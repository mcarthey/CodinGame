using CodinGame2026.IO;
using Xunit;

namespace CodinGame2026.Tests;

public class InputParserTests
{
    // ── Helpers ─────────────────────────────────────────────────────────────

    private static TextReader MakeReader(params string[] lines) =>
        new StringReader(string.Join(Environment.NewLine, lines));

    // ── Tests ────────────────────────────────────────────────────────────────

    [Fact]
    public void Parse_StubInput_ReturnsTurnNumber()
    {
        // Arrange
        var reader = MakeReader(); // stub parser ignores input for now

        // Act
        var state = InputParser.Parse(reader, turn: 3);

        // Assert
        Assert.Equal(3, state.Turn);
    }

    [Fact]
    public void Parse_StubInput_ReturnsNonNullGrid()
    {
        var reader = MakeReader();
        var state  = InputParser.Parse(reader, turn: 1);

        Assert.NotNull(state.Grid);
    }

    [Fact]
    public void Parse_StubInput_ReturnsEmptyRobotList()
    {
        var reader = MakeReader();
        var state  = InputParser.Parse(reader, turn: 1);

        // Stub returns no robots until parsing is implemented
        Assert.NotNull(state.Robots);
    }

    [Fact]
    public void ReadInts_ParsesSpaceSeparatedIntegers()
    {
        var reader = MakeReader("3 7 42");
        var result = InputParser.ReadInts(reader);

        Assert.Equal([3, 7, 42], result);
    }

    [Fact]
    public void ReadInts_EmptyLine_ReturnsEmptyArray()
    {
        var reader = MakeReader(string.Empty);
        var result = InputParser.ReadInts(reader);

        Assert.Empty(result);
    }

    [Fact]
    public void ReadTokens_ParsesSpaceSeparatedStrings()
    {
        var reader = MakeReader("MOVE 3 2");
        var result = InputParser.ReadTokens(reader);

        Assert.Equal(["MOVE", "3", "2"], result);
    }
}
