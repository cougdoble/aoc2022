using AOC2022.Utils;

namespace AOC2022.Solutions.Day08;

public class Solution : SolutionBase
{
    private readonly List<int[]> _data;

    public Solution() : base(08, 2022, "Treetop Tree House")
    {
        _data = new List<int[]>();
        Parse(Input.SplitByNewline(true).ToList());
    }

    protected override string SolvePartOne()
    {
        var sum = 0;
        var dim = _data.Count - 1;
        var visible = _data.Select(_ => new int[_data[0].Length]).ToList();

        for (var i = 0; i < _data.Count; i++)
        {
            var maxLeft = 0;
            var maxRight = 0;
            var maxTop = 0;
            var maxBottom = 0;
            for (var j = 0; j < _data.Count; j++)
            {
                if (_data[i][j] > maxLeft)
                {
                    maxLeft = _data[i][j];
                    visible[i][j] = 1;
                }

                if (_data[i][dim - j] > maxRight)
                {
                    maxRight = _data[i][dim - j];
                    visible[i][dim - j] = 1;
                }

                if (_data[j][i] > maxTop)
                {
                    maxTop = _data[j][i];
                    visible[j][i] = 1;
                }

                if (_data[dim - j][i] > maxBottom)
                {
                    maxBottom = _data[dim - j][i];
                    visible[dim - j][i] = 1;
                }
            }
        }

        for (var i = 0; i < _data.Count; i++)
        for (var j = 0; j < _data.Count; j++)
            sum += visible[i][j];
        return sum.ToString();
    }

    protected override string SolvePartTwo()
    {
        long max = 0;
        var len = _data.Count;
        for (var i = 0; i < _data.Count; i++)
        for (var j = 0; j < _data.Count; j++)
        {
            int tmp = 0, prod = 1;
            for (var k = i - 1; k >= 0; k--)
            {
                tmp++;
                if (_data[k][j] >= _data[i][j]) break;
            }

            prod *= tmp;
            tmp = 0;
            for (var k = i + 1; k < len; k++)
            {
                tmp++;
                if (_data[k][j] >= _data[i][j]) break;
            }

            prod *= tmp;
            tmp = 0;
            for (var k = j - 1; k >= 0; k--)
            {
                tmp++;
                if (_data[i][k] >= _data[i][j]) break;
            }

            prod *= tmp;
            tmp = 0;
            for (var k = j + 1; k < len; k++)
            {
                tmp++;
                if (_data[i][k] >= _data[i][j]) break;
            }

            prod *= tmp;
            if (prod > max) max = prod;
        }

        return max.ToString();
    }

    private void Parse(List<string> input)
    {
        foreach (var s in input)
        {
            var arr = new int[s.Length];
            for (var i = 0; i < s.Length; i++) arr[i] = s[i] - '0' + 1;
            _data.Add(arr);
        }
    }
}