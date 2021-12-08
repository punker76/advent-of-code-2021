namespace AdventOfCode;

public class Day_08 : BaseDay
{
    private class Digit
    {
        public string segments;
        public int number = -1;

        public Digit(string segments)
        {
            this.segments = segments;
            this.number = segments.Length
            switch
            {
                2 => 1,
                4 => 4,
                3 => 7,
                7 => 8,
                _ => -1
            };
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
        public long output;

        public Entry(string input)
        {
            var splitted = input.Split(" | ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            this.uniqueSignalPatterns = splitted[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new Digit(x))
                .ToList();

            this.fourDigitOutputValue = splitted[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new Digit(x))
                .ToList();

            var uniqueNumbers = uniqueSignalPatterns.Where(d => d.IsUnique()).OrderBy(d => d.number).ToList();
            var sixSegmentNumbers = uniqueSignalPatterns.Where(d => d.segments.Length == 6).ToList();
            var fiveSegmentNumbers = uniqueSignalPatterns.Where(d => d.segments.Length == 5).ToList();

            var oneDigit = uniqueNumbers[0];
            var fourDigit = uniqueNumbers[1];
            var sevenDigit = uniqueNumbers[2];
            var eightDigit = uniqueNumbers[3];

            var one = oneDigit.segments.ToCharArray();
            var four = fourDigit.segments.ToCharArray();
            var seven = sevenDigit.segments.ToCharArray();
            var eight = eightDigit.segments.ToCharArray();

            var nineDigit = sixSegmentNumbers.FirstOrDefault(d => d.segments.ToCharArray().Except(four.Concat(seven)).Count() == 1);
            var sixDigit = sixSegmentNumbers.Except(new [] { nineDigit }).FirstOrDefault(d => d.segments.ToCharArray().Except(four.Except(one)).Count() == 4);
            var zeroDigit = sixSegmentNumbers.Except(new [] { nineDigit, sixDigit }).FirstOrDefault();

            var nine = nineDigit.segments.ToCharArray();
            var six = sixDigit.segments.ToCharArray();
            var zero = zeroDigit.segments.ToCharArray();

            var twoDigit = fiveSegmentNumbers.FirstOrDefault(d => d.segments.ToCharArray().Union(four).Count() == 7);
            var fiveDigit = fiveSegmentNumbers.Except(new [] { twoDigit }).FirstOrDefault(d => d.segments.ToCharArray().Union(seven).Count() == 6);
            var threeDigit = fiveSegmentNumbers.Except(new [] { twoDigit, fiveDigit }).FirstOrDefault();

            var two = twoDigit.segments.ToCharArray();
            var five = fiveDigit.segments.ToCharArray();
            var three = threeDigit.segments.ToCharArray();

            var digits = new [] { zero, one, two, three, four, five, six, seven, eight, nine }.ToList();
            this.output = int.Parse(string.Join("", fourDigitOutputValue.Select(o => digits.FindIndex(d => d.Length == o.segments.Length && d.Except(o.segments.ToCharArray()).Count() == 0))));
        }
    }

    private readonly string[] _input;
    private readonly List<Entry> _entries;

    public Day_08()
    {
        _input = File.ReadAllLines(InputFilePath);
        // _input = new [] { "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf" };
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
        var result = _entries.Sum(e => e.output);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}