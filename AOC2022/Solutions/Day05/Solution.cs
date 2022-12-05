namespace AOC2022.Solutions.Day05;

public class Solution : SolutionBase
{
    public Solution() : base(05, 2022, "Supply Stacks")
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(Input, false);
    }

    protected override string SolvePartTwo()
    {
        return Solve(Input, true);
    }

    private static string Solve(string input, bool part2)
    {
        return string.Concat(
            input
                .Substring(input.IndexOf("\n\n") + 2)
                .Split("\n")
                .Select(line => line.Split(" "))
                .Select(segments =>
                    (int.Parse(segments[1]), int.Parse(segments[3]), int.Parse(segments[5])))
                .Aggregate(ParseInitialStacks(input), (s, m) => MakeMovements(part2, s, m))
                .Select(s => s.Pop()));
    }

    private static Stack<char>[] MakeMovements(bool part2, Stack<char>[] stacks, (int, int, int) movements)
    {
        return (part2 ? RetrieveCrates(stacks, movements).Reverse() : RetrieveCrates(stacks, movements))
            .Aggregate(stacks, (_, item) =>
            {
                stacks[movements.Item3 - 1].Push(item);

                return stacks;
            });
    }

    private static IEnumerable<char> RetrieveCrates(Stack<char>[] stacks, (int, int, int) movements)
    {
        return Enumerable
            .Range(0, movements.Item1)
            .Select(_ => stacks[movements.Item2 - 1].Pop());
    }

    private static Stack<char>[] ParseInitialStacks(string input)
    {
        return input
            .Substring(0, input.IndexOf("\n\n"))
            .Split("\n")
            .SelectMany(line => line.Select((ch, idx) => (ch, idx)))
            .GroupBy(t => t.idx / 4)
            .Select(g => new Stack<char>(g.Select(t => t.ch).Where(char.IsLetter).Reverse()))
            .ToArray();
    }
}