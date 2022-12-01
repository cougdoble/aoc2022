using System.Text.Json;
using System.Text.Json.Serialization;

namespace AOC.Services;

internal struct Config
{
    public string Cookie { get; set; }

    public int Year { get; set; }

    [JsonConverter(typeof(DaysConverter))] public int[] Days { get; set; }

    public void SetDefaults()
    {
        //Make sure we're looking at EST, or it might break for most of the US
        var currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
        Cookie ??= "";
        if (Year == default) Year = currentEst.Year;
        Days ??= currentEst.Month == 12 && currentEst.Day <= 25 ? new[] { currentEst.Day } : new[] { 0 };
    }
}

internal class DaysConverter : JsonConverter<int[]>
{
    public override int[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IEnumerable<string> tokens;

        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return new int[] { reader.GetInt16() };

            case JsonTokenType.String:
                tokens = new[] { reader.GetString() ?? "" };
                break;

            default:
                var obj = JsonSerializer
                    .Deserialize<object[]>(ref reader);

                tokens = obj != null
                    ? obj.Select<object, string>(o => o.ToString() ?? "")
                    : new string[] { };
                break;
        }

        var days = tokens.SelectMany(ParseString);
        return days.Contains(0) ? new[] { 0 } : days.Where(v => v is < 26 and > 0).OrderBy(day => day).ToArray();
    }

    private static IEnumerable<int> ParseString(string str)
    {
        return str.Split(",").SelectMany(str =>
        {
            if (!str.Contains("..")) return int.TryParse(str, out var day) ? new[] { day } : Array.Empty<int>();
            var split = str.Split("..");
            var start = int.Parse(split[0]);
            var stop = int.Parse(split[1]);
            return Enumerable.Range(start, stop - start + 1);
        });
    }

    public override void Write(Utf8JsonWriter writer, int[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var val in value) writer.WriteNumberValue(val);
        writer.WriteEndArray();
    }
}