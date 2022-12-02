using AOC2022.Utils;

namespace AOC2022.Solutions.Day02;

public class Solution : SolutionBase
{
    private readonly string[] _rockPaperScissors;

    public Solution() : base(02, 2022, "Rock Paper Scissors")
    {
        _rockPaperScissors = Input.SplitByNewline(true);
    }

    protected override string SolvePartOne()
    {
        var totalScore = _rockPaperScissors.Sum(round => GetRoundScore(round, false));
        return totalScore.ToString();
    }

    protected override string SolvePartTwo()
    {
        var totalScore = _rockPaperScissors.Sum(round => GetRoundScore(round, true));
        return totalScore.ToString();
    }

    private static int GetRoundScore(string roundDef, bool guideExplained)
    {
        (char playerOne, char playerTwo) round = (roundDef[
            0], roundDef[
            2]);

        return round switch
        {
            ('A', 'X') => guideExplained ? 3 : 4,
            ('A', 'Y') => guideExplained ? 4 : 8,
            ('A', 'Z') => guideExplained ? 8 : 3,
            ('B', 'X') => 1,
            ('B', 'Y') => 5,
            ('B', 'Z') => 9,
            ('C', 'X') => guideExplained ? 2 : 7,
            ('C', 'Y') => guideExplained ? 6 : 2,
            ('C', 'Z') => guideExplained ? 7 : 6,
            _ => throw new NotImplementedException()
        };
    }
}