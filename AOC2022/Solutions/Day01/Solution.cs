namespace AOC2022.Solutions.Day01;

public class Solution : SolutionBase
{
    private readonly string[] _calories;

    public Solution() : base(01, 2022, "Calorie Counting", false)
    {
        _calories = Input.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None)
            .ToArray();
    }

    protected override string SolvePartOne()
    {
        var elvesByTotalCal = GetCalorieTotals(_calories);

        return elvesByTotalCal.FirstOrDefault().ToString();
    }

    protected override string SolvePartTwo()
    {
        var elvesByTotalCal = GetCalorieTotals(_calories);
        return elvesByTotalCal.Take(3).Sum().ToString();
    }

    private static IEnumerable<int> GetCalorieTotals(IEnumerable<string> rawCalorieList)
    {
        var elfCalorieTotals = new List<int>();
        var elfCalTotal = 0;
        foreach (var cal in rawCalorieList)
            if (string.IsNullOrEmpty(cal))
            {
                if (elfCalTotal <= 0) continue;
                elfCalorieTotals.Add(elfCalTotal);
                elfCalTotal = 0;
            }
            else
            {
                elfCalTotal += int.Parse(cal);
            }

        return elfCalorieTotals.OrderByDescending(i => i).ToList();
    }
}