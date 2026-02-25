using CodinGame2026.AI;
using CodinGame2026.IO;

// ── Entry point ─────────────────────────────────────────────────────────────
// CodinGame standard game loop:
//   • Read one turn of input from stdin via InputParser
//   • Compute the best action via the strategy
//   • Write the action to stdout
//
// Keep this file thin — all logic lives in AI/ and IO/.

var strategy = new BotStrategy();
int turn = 1;

while (true)
{
    var state  = InputParser.Parse(Console.In, turn);
    var action = strategy.GetAction(state);
    Console.WriteLine(action);
    turn++;
}
