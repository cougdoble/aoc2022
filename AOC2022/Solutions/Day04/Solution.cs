using AOC2022.Utils;

namespace AOC2022.Solutions.Day04;

public class Solution : SolutionBase
{
    private readonly string[] _input;

    public Solution() : base(04, 2022, "Camp Cleanup")
    {
        _input = Input.SplitByNewline(true);
    }

    protected override string SolvePartOne()
    {
        return FindRedundantSets(true);
    }

    protected override string SolvePartTwo()
    {
        return FindRedundantSets(false);
    }

    private string FindRedundantSets(bool fullyContainedSets)
    {
        var redundantSets = (from i in _input
            select i.Split(",")
            into set
            let set1 = set[0].Split("-")
            let set1StartVal = int.Parse(set1[0])
            let set1EndVal = int.Parse(set1[1])
            let set2 = set[1].Split("-")
            let set2StartVal = int.Parse(set2[0])
            let set2EndVal = int.Parse(set2[1])
            let range1 = Enumerable.Range(set1StartVal, set1EndVal - set1StartVal + 1).ToArray()
            let range2 = Enumerable.Range(set2StartVal, set2EndVal - set2StartVal + 1).ToArray()
            where fullyContainedSets
                ? range1.All(range2.Contains) || range2.All(range1.Contains)
                : range1.Any(range2.Contains) || range2.Any(range1.Contains)
            select range1).Count();

        return redundantSets.ToString();
    }
}