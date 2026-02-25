// Shadows of the Knight – Episode 1 (Code Size)
// Algorithm: 2D binary search — shrink bounding box with each direction hint,
// then jump to the midpoint of the remaining box.
//
// Golfed: run `pwsh ../../tools/merge.ps1` isn't needed here — this is already
// a single file. Copy everything below the separator and paste into CodinGame.
//
// ── SUBMISSION (paste this) ──────────────────────────────────────────────────

var t=Console.ReadLine()!.Split();int W=int.Parse(t[0]),H=int.Parse(t[1]),x0=0,y0=0,x1=W-1,y1=H-1;Console.ReadLine();t=Console.ReadLine()!.Split();int x=int.Parse(t[0]),y=int.Parse(t[1]);for(;;){var d=Console.ReadLine()!;if(d.Contains('U'))y1=y-1;if(d.Contains('D'))y0=y+1;if(d.Contains('L'))x1=x-1;if(d.Contains('R'))x0=x+1;Console.WriteLine($"{x=(x0+x1)/2} {y=(y0+y1)/2}");}

// ── READABLE VERSION (same logic, expanded) ──────────────────────────────────
//
// var parts = Console.ReadLine()!.Split();
// int W  = int.Parse(parts[0]);
// int H  = int.Parse(parts[1]);
// Console.ReadLine();                          // N (jumps) — unused in pure binary search
// parts  = Console.ReadLine()!.Split();
// int x  = int.Parse(parts[0]);
// int y  = int.Parse(parts[1]);
//
// // Bounding box that must contain the bomb
// int x0 = 0, y0 = 0, x1 = W - 1, y1 = H - 1;
//
// while (true)
// {
//     string dir = Console.ReadLine()!;
//
//     if (dir.Contains('U')) y1 = y - 1;   // bomb is strictly above
//     if (dir.Contains('D')) y0 = y + 1;   // bomb is strictly below
//     if (dir.Contains('L')) x1 = x - 1;   // bomb is strictly left
//     if (dir.Contains('R')) x0 = x + 1;   // bomb is strictly right
//
//     x = (x0 + x1) / 2;
//     y = (y0 + y1) / 2;
//     Console.WriteLine($"{x} {y}");
// }
