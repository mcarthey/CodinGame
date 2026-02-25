# Shadows of the Knight – Episode 1 (Code Size)

**Difficulty:** Medium
**Type:** Code golf (minimize character count)
**URL:** https://www.codingame.com/ide/puzzle/shadows-of-the-knight-episode-1-codesize

---

## Story

Batman is hunting for hostages in a building full of windows. He can jump between
windows using his grapnel gun, but the bombs go off after a fixed number of jumps.
A heat-signature device tells him the *direction* of the hostages after each jump.
Find them in as few jumps as possible — but since this is code size, write the
solution in as few *characters* as possible.

---

## Rules

The building is a `W × H` grid of windows. `(0, 0)` is the top-left corner.
X increases to the right; Y increases downward.

Each turn the device returns a compass direction:

| String | Meaning |
|--------|---------|
| `U`    | bomb is above current position (Y decreases) |
| `D`    | bomb is below (Y increases) |
| `L`    | bomb is to the left (X decreases) |
| `R`    | bomb is to the right (X increases) |
| `UR`, `UL`, `DR`, `DL` | diagonal combinations of the above |

---

## Input / Output

### Initialization (read once)
```
W H       ← building width and height (windows)
N         ← max jumps allowed
X0 Y0     ← Batman's starting position
```

### Each turn
**Input:** one direction string (e.g. `UL`, `R`, `D`)
**Output:** `X Y` — the window Batman jumps to next

---

## Constraints

- `1 ≤ W, H ≤ 10 000`
- `2 ≤ N ≤ 100`
- Response time per turn ≤ 150 ms

---

## Algorithm

**2D Binary Search.**

Maintain a bounding box `(x0,y0)–(x1,y1)` that is guaranteed to contain the bomb.
Each turn's direction shrinks one or both axes of the box, then jump to the midpoint.

```
direction contains 'U' → y1 = currentY - 1   (bomb is strictly above)
direction contains 'D' → y0 = currentY + 1   (bomb is strictly below)
direction contains 'L' → x1 = currentX - 1
direction contains 'R' → x0 = currentX + 1
next position → ((x0+x1)/2, (y0+y1)/2)
```

Worst-case jumps needed: `log2(10000) ≈ 14`, which fits within the 14-jump
test case — confirming pure binary search is optimal.

---

## Code Golf Notes

- No abstractions needed — everything fits in one `for(;;)` loop
- `string.Contains(char)` is shorter than splitting on every direction character
- Inline the assignment inside `Console.WriteLine` to save a line
- `var` everywhere; avoid explicit types
- C# is verbose for golf — Python/Ruby would be ~2× shorter, but this is a C# repo

---

## Solution

See [`Program.cs`](./Program.cs) in this directory.
