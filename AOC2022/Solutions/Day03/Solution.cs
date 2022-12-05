using AOC2022.Utils;

namespace AOC2022.Solutions.Day03;

public class Solution : SolutionBase
{
    private readonly string[] _compartments;
    private readonly List<char> _itemsMap;

    public Solution() : base(03, 2022, "Rucksack Reorganization")
    {
        _compartments = Input.SplitByNewline(true);

        var itemsMap = Enumerable.Range('a', 'z' - 'a' + 1).Select(x => (char)x).ToList();
        itemsMap.AddRange(Enumerable.Range('A', 'Z' - 'A' + 1).Select(x => (char)x).ToList());

        _itemsMap = itemsMap;
    }

    protected override string SolvePartOne()
    {
        var prioritySum = 0;

        foreach (var compartmentGroup in _compartments)
        {
            var compartmentA = compartmentGroup.Take(compartmentGroup.Length / 2).ToArray();
            var compartmentB = compartmentGroup.TakeLast(compartmentGroup.Length / 2).ToArray();

            var priorityItem = compartmentA.First(itemA => compartmentB.Contains(itemA));

            var priorityValue = _itemsMap.IndexOf(priorityItem) + 1;
            prioritySum += priorityValue;
        }

        return prioritySum.ToString();
    }

    protected override string SolvePartTwo()
    {
        var sum = 0;
        for (var i = 0; i < _compartments.ToList().Count; i += 3)
        {
            var p = ' ';
            var counts = new Dictionary<char, int>();
            foreach (char c in _compartments[i]) counts[c] = counts.GetValueOrDefault(c) | 1;
            foreach (char c in _compartments[i + 1]) counts[c] = counts.GetValueOrDefault(c) | 2;
            foreach (char c in _compartments[i + 2])
            {
                counts[c] = counts.GetValueOrDefault(c) | 4;
                if (counts[c] == 7) p = c;
            }

            if (p > 'a') sum += p - 'a' + 1;
            else sum += p - 'A' + 27;
        }

        return sum.ToString();
        /*
        var elfGroup = new List<List<string>>();
        var compartmentSet = new List<string>();
        var groupCount = 0;

        foreach (var compartmentGroup in _compartments)
        {
            //Console.WriteLine(compartmentGroup);
            if (groupCount < 3)
            {
                compartmentSet.Add(compartmentGroup);
                //Console.WriteLine($"added {compartmentGroup} -- set size: {compartmentSet.Count}");
                groupCount++;
            }
            else
            {
                elfGroup.Add(compartmentSet);
                compartmentSet = new List<string> { compartmentGroup };
                //Console.WriteLine($"added {compartmentGroup} -- set size: {compartmentSet.Count}");
                groupCount = 0;
            }
            //Console.WriteLine(elfGroup.Count);
        }
        elfGroup.Add(compartmentSet);
        //Console.WriteLine($"elf groups: {elfGroup.Count}");

        var elfBadges = new Dictionary<char, int>();
        foreach (var set in elfGroup)
        {
            var matches = new List<char>();
            var set1 = set.First().Distinct().ToList();
            set1.ForEach(c =>
            {
                //Console.WriteLine(c);
                if (set[1].Contains(c) && set[2].Contains(c))
                {
                    matches.Add(c);
                }
            });
            
            // foreach (var match in matches)
            // {
            //     Console.WriteLine(match);
            // }
            var badge = matches.Max();
            //Console.WriteLine(badge);
            //Console.WriteLine(_itemsMap.IndexOf(badge));
            
            elfBadges.TryAdd(badge, _itemsMap.IndexOf(badge) + 1);
        }

        //foreach (var c in elfGroup) Console.WriteLine($"{String.Join(",", c)}");
        
        return elfBadges.Sum(b => b.Value).ToString();
    */
    }
}