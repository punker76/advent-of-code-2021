namespace AdventOfCode;

public class Day_05 : BaseDay
{
    [DebuggerDisplay("{X},{Y} = {CoversLine}")]
    private class Point
    {
        public int X = 0;
        public int Y = 0;
        public int CoversLine = 0;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    private class Line
    {
        public Point p1;
        public Point p2;

        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public bool IsPointOnLine(Point point)
        {
            if (p1.X == p2.X && point.X == p1.X)
            {
                return (point.Y - p1.Y) * (p2.Y - point.Y) >= 0;
            }
            else if (p1.Y == p2.Y && point.Y == p1.Y)
            {
                return (point.X - p1.X) * (p2.X - point.X) >= 0;

            }

            return false;
        }
    }

    private readonly string[] _input;
    // private readonly int[] _numbers;
    private readonly List<Line> _lines;
    private readonly string pattern = @"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)";
    private int _maxX = 0;
    private int _maxY = 0;

    public Day_05()
    {
        _input = File.ReadAllLines(InputFilePath);

        _lines = _input
            .Select(l =>
            {
                var matches = Regex.Matches(l, pattern);

                var p1 = new Point(int.Parse(matches[0].Groups["x1"].Value), int.Parse(matches[0].Groups["y1"].Value));
                var p2 = new Point(int.Parse(matches[0].Groups["x2"].Value), int.Parse(matches[0].Groups["y2"].Value));

                _maxX = Math.Max(_maxX, Math.Max(p1.X, p2.X));
                _maxY = Math.Max(_maxY, Math.Max(p1.Y, p2.Y));

                return new Line(p1, p2);
            })
            .ToList();

        // Console.WriteLine($"max X = {_maxX}, maxY = {_maxY}");
    }

    public override ValueTask<string> Solve_1()
    {
        var points = new List<Point>();

        for (int x = 0; x <= _maxX; x++)
        {
            for (int y = 0; y <= _maxX; y++)
            {
                var point = new Point(x, y);
                point.CoversLine = _lines.Count(l => l.IsPointOnLine(point));
                points.Add(point);
            }
        }

        var result = points.Count(p => p.CoversLine >= 2); // 3990

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}