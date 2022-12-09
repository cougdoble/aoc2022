using AOC2022.Utils;

namespace AOC2022.Solutions.Day09;

public class Solution : SolutionBase
{
    private readonly string[] _input;

    public Solution() : base(09, 2022, "Rope Bridge")
    {
        _input = Input.SplitByNewline(true);
    }

    protected override string SolvePartOne()
    {
        return Solve(2);
    }

    protected override string SolvePartTwo()
    {
        return Solve(10);
    }

    private string Solve(int ropeLength)
    {
        (int x, int y)[] rope = new (int, int)[ropeLength];
        var tailVisited = new HashSet<(int, int)> { rope[^1] };

        foreach (var i in _input)
        {
            var instruction = i.Split();
            if (int.TryParse(instruction?[1], out var moves))
                foreach (var _ in Enumerable.Range(0, moves))
                {
                    var head = rope[0];
                    head = instruction[0] switch
                    {
                        "U" => (head.x, head.y + 1),
                        "D" => (head.x, head.y - 1),
                        "L" => (head.x - 1, head.y),
                        "R" => (head.x + 1, head.y),
                        _ => head
                    };
                    rope[0] = head;
                    foreach (var j in Enumerable.Range(1, rope.Length - 1))
                    {
                        var headKnot = rope[j - 1];
                        var tailKnot = rope[j];
                        var distance = (headKnot.x - tailKnot.x, headKnot.y - tailKnot.y);
                        rope[j] = distance switch
                        {
                            (2, 2) => (tailKnot.x + 1, tailKnot.y + 1),
                            (-2, -2) => (tailKnot.x - 1, tailKnot.y - 1),
                            (-2, 2) => (tailKnot.x - 1, tailKnot.y + 1),
                            (2, -2) => (tailKnot.x + 1, tailKnot.y - 1),
                            (var x, -2) => (tailKnot.x + x, tailKnot.y - 1),
                            (-2, var y) => (tailKnot.x - 1, tailKnot.y + y),
                            (2, var y) => (tailKnot.x + 1, tailKnot.y + y),
                            (var x, 2) => (tailKnot.x + x, tailKnot.y + 1),
                            _ => tailKnot
                        };
                    }
                    tailVisited.Add(rope[^1]);
                }
        }

        return tailVisited.Count.ToString();
    }
}