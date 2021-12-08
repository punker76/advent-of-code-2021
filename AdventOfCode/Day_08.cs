namespace AdventOfCode;

public class Day_08 : BaseDay
{
    private class Digit
    {
        public string segments;

        public Digit(string segments)
        {
            this.segments = segments;
        }

        public bool IsUnique()
        {
            return this.segments.Length == 2 ||
                this.segments.Length == 3 ||
                this.segments.Length == 4 ||
                this.segments.Length == 7;
        }
    }

    private class Entry
    {
        public List<Digit> uniqueSignalPatterns;
        public List<Digit> fourDigitOutputValue;

        public Entry(string input)
        {
            var splitted = input.Split(" | ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            uniqueSignalPatterns = splitted[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new Digit(x))
                .ToList();

            fourDigitOutputValue = splitted[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new Digit(x))
                .ToList();
        }
    }

    private readonly string[] _input;
    private readonly List<Entry> _entries;

    public Day_08()
    {
        _input = File.ReadAllLines(InputFilePath);
        _entries = _input
            .Select(e => new Entry(e))
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = _entries.Sum(e => e.fourDigitOutputValue.Count(d => d.IsUnique()));

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}