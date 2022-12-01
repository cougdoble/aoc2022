using System.Diagnostics;
using System.Net;
using AOC.Services;

namespace AOC2022;

public abstract class SolutionBase
{
    private protected SolutionBase(int day, int year, string title, bool useDebugInput = false)
    {
        Day = day;
        Year = year;
        Title = title;
        Debug = useDebugInput;
    }

    public int Day { get; }
    public int Year { get; }
    public string Title { get; }
    public bool Debug { get; set; }
    public string Input => LoadInput(Debug);
    public string DebugInput => LoadInput(true);

    public SolutionResult Part1 => Solve();
    public SolutionResult Part2 => Solve(2);

    public IEnumerable<SolutionResult> SolveAll()
    {
        yield return Solve(SolvePartOne);
        yield return Solve(SolvePartTwo);
    }

    public SolutionResult Solve(int part = 1)
    {
        if (part == 1) return Solve(SolvePartOne);
        if (part == 2) return Solve(SolvePartTwo);

        throw new InvalidOperationException("Invalid part param supplied.");
    }

    private SolutionResult Solve(Func<string> SolverFunction)
    {
        if (Debug)
        {
            if (string.IsNullOrEmpty(DebugInput)) throw new Exception("DebugInput is null or empty");
        }
        else if (string.IsNullOrEmpty(Input))
        {
            throw new Exception("Input is null or empty");
        }

        try
        {
            var then = DateTime.Now;
            var result = SolverFunction();
            var now = DateTime.Now;
            return string.IsNullOrEmpty(result)
                ? SolutionResult.Empty
                : new SolutionResult { Answer = result, Time = now - then };
        }
        catch (Exception)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                return SolutionResult.Empty;
            }

            throw;
        }
    }

    private string LoadInput(bool debug = false)
    {
        var inputFilepath =
            $"./Solutions/Day{Day:D2}/{(debug ? "debug.txt" : "input.txt")}";

        if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
        {
            return File.ReadAllText(inputFilepath);
        }
        Console.WriteLine("file not found");

        if (debug) return "";

        try
        {
            var input = AocService.FetchInput(Year, Day).Result;
            File.WriteAllText(inputFilepath, input);
            return input;
        }
        catch (HttpRequestException e)
        {
            var code = e.StatusCode;
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (code == HttpStatusCode.BadRequest)
            {
                Console.WriteLine(
                    $"Day {Day}: Received 400 when attempting to retrieve puzzle input. Your session cookie is probably not recognized.");
            }
            else if (code == HttpStatusCode.NotFound)
            {
                Console.WriteLine(
                    $"Day {Day}: Received 404 when attempting to retrieve puzzle input. The puzzle is probably not available yet.");
            }
            else
            {
                Console.ForegroundColor = colour;
                Console.WriteLine(e.ToString());
            }

            Console.ForegroundColor = colour;
        }
        catch (InvalidOperationException)
        {
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Day {Day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
            Console.ForegroundColor = colour;
        }

        return "";
    }

    public override string ToString()
    {
        return $"\n--- Day {Day}: {Title} --- {(Debug ? "!! Debug mode active, using DebugInput !!" : "")}\n"
               + $"{ResultToString(1, Part1)}\n"
               + $"{ResultToString(2, Part2)}";
    }

    private string ResultToString(int part, SolutionResult result)
    {
        return $"  - Part{part} => " + (string.IsNullOrEmpty(result.Answer)
            ? "Unsolved"
            : $"{result.Answer} ({result.Time.TotalMilliseconds}ms)");
    }

    protected abstract string SolvePartOne();
    protected abstract string SolvePartTwo();
}

public struct SolutionResult
{
    public string Answer { get; set; }
    public TimeSpan Time { get; set; }

    public static SolutionResult Empty => new();
}