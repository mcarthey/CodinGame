// Shadows of the Knight – Episode 1 (Code Size)
// Algorithm: 2D binary search — shrink bounding box with each direction hint,
// then jump to the midpoint of the remaining box.
//
// Golfed: run `pwsh ../../tools/merge.ps1` isn't needed here — this is already
// a single file. Copy everything below the separator and paste into CodinGame.
//
// ── SUBMISSION v1 — naive (~389 chars) ───────────────────────────────────────
// Baseline: no tricks, just correct logic.

var t=Console.ReadLine()!.Split();int W=int.Parse(t[0]),H=int.Parse(t[1]),x0=0,y0=0,x1=W-1,y1=H-1;Console.ReadLine();t=Console.ReadLine()!.Split();int x=int.Parse(t[0]),y=int.Parse(t[1]);for(;;){var d=Console.ReadLine()!;if(d.Contains('U'))y1=y-1;if(d.Contains('D'))y0=y+1;if(d.Contains('L'))x1=x-1;if(d.Contains('R'))x0=x+1;Console.WriteLine($"{x=(x0+x1)/2} {y=(y0+y1)/2}");}

// ── SUBMISSION v2 — with aliases (~346 chars, saves ~43) ─────────────────────
// Trick 1: `using static System.Console` drops the `Console.` prefix everywhere.
// Trick 2: local functions R() and P() alias the two longest repeated expressions.
// Trick 3: direction chars are always at a fixed index — no need for Contains():
//   U/D are always at index 0  →  d[0]=='U'
//   L/R are always the last char  →  d[^1]=='L'  (^1 = index from end)

using static System.Console;string R()=>ReadLine()!;int P(string s)=>int.Parse(s);var t=R().Split();int W=P(t[0]),H=P(t[1]),x0=0,y0=0,x1=W-1,y1=H-1;R();t=R().Split();int x=P(t[0]),y=P(t[1]);for(;;){var d=R();if(d[0]=='U')y1=y-1;if(d[0]=='D')y0=y+1;if(d[^1]=='L')x1=x-1;if(d[^1]=='R')x0=x+1;WriteLine($"{x=(x0+x1)/2} {y=(y0+y1)/2}");}


// ── CODINGAME STARTER (what the site gives you) ──────────────────────────────
//
// using System;
// using System.Linq;
// using System.IO;
// using System.Text;
// using System.Collections;
// using System.Collections.Generic;
//
// class Player
// {
//     static void Main(string[] args)
//     {
//         string[] inputs;
//         inputs = Console.ReadLine().Split(' ');
//         int W = int.Parse(inputs[0]); // width of the building.
//         int H = int.Parse(inputs[1]); // height of the building.
//         int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
//         inputs = Console.ReadLine().Split(' ');
//         int X0 = int.Parse(inputs[0]);
//         int Y0 = int.Parse(inputs[1]);
//         while (true)
//         {
//             string bombDir = Console.ReadLine(); // U, UR, R, DR, D, DL, L or UL
//             Console.WriteLine("0 0");
//         }
//     }
// }

// ── READABLE VERSION (v2 logic, expanded) ────────────────────────────────────
//
// var parts = Console.ReadLine()!.Split();
// int W  = int.Parse(parts[0]);
// int H  = int.Parse(parts[1]);
// Console.ReadLine();                          // N (jumps) — unused in pure binary search
// parts  = Console.ReadLine()!.Split();
// int x  = int.Parse(parts[0]);
// int y  = int.Parse(parts[1]);
//
// int x0 = 0, y0 = 0, x1 = W - 1, y1 = H - 1;
//
// while (true)
// {
//     string dir = Console.ReadLine()!;
//
//     // U/D are always at index 0; L/R are always the last character
//     if (dir[0]   == 'U') y1 = y - 1;
//     if (dir[0]   == 'D') y0 = y + 1;
//     if (dir[^1]  == 'L') x1 = x - 1;
//     if (dir[^1]  == 'R') x0 = x + 1;
//
//     x = (x0 + x1) / 2;
//     y = (y0 + y1) / 2;
//     Console.WriteLine($"{x} {y}");
// }
