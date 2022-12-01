namespace AOC2022;

public static class SolutionCollector
{
    public static IEnumerable<SolutionBase> FetchSolutions(int year, IEnumerable<int> days)
    {
        if (days.Sum() == 0) days = Enumerable.Range(1, 25).ToArray();

        foreach (var day in days)
        {
            var type = Type.GetType($"AOC2022.Solutions.Day{day:D2}.Solution");
            if (type == null) continue;
            if (Activator.CreateInstance(type) is SolutionBase solution) yield return solution;
        }
    }
}