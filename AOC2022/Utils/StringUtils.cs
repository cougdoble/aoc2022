namespace AOC2022.Utils;

public static class StringUtils
{
    public static string[] SplitByNewline(this string str, bool shouldTrim = false)
    {
        return str
            .Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => shouldTrim ? s.Trim() : s)
            .ToArray();
    }
}