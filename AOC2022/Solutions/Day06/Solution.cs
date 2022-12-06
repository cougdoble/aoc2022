namespace AOC2022.Solutions.Day06;

public class Solution : SolutionBase
{
    public Solution() : base(06, 2022, "")
    {
    }

    protected override string SolvePartOne()
    {
        return Solve(4);
    }

    protected override string SolvePartTwo()
    {
        return Solve(14);
    }

    private string Solve(int sequenceLength)
    {
        var input = Input.Trim();

        var previousValues = new List<char>();

        var charCount = 0;

        var idx = 0;
        foreach (var c in input)
        {
            ++idx;
            previousValues.Add(c);
            if (previousValues.Count >= sequenceLength)
            {
                Console.WriteLine(string.Join(", ", previousValues));
                var seq = input.Substring(idx - sequenceLength, sequenceLength).ToList();

                if (seq.GroupBy(x => x).Select(x => new { Value = x.Key, Count = x.Count() })
                        .Where(x => x.Count > 1).ToList().Count == 0)
                {
                    Console.WriteLine($"sub -- {string.Join(", ", seq)} -- {c}");
                    charCount = idx;
                    Console.WriteLine(charCount);
                }

                if (charCount > 0) break;
            }
        }

        return previousValues.Count.ToString();
    }
}