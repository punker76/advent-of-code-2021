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
            if (p1.X == p2.X)
            {
                return point.X == p1.X && (point.Y - p1.Y) * (p2.Y - point.Y) >= 0;
            }
            else if (p1.Y == p2.Y)
            {
                return point.Y == p1.Y && (point.X - p1.X) * (p2.X - point.X) >= 0;

            }

            return false;
        }

        public bool IsPointOnLine2(Point point)
        {
            if (p1.X == p2.X)
            {
                return point.X == p1.X && (point.Y - p1.Y) * (p2.Y - point.Y) >= 0;
            }
            else if (p1.Y == p2.Y)
            {
                return point.Y == p1.Y && (point.X - p1.X) * (p2.X - point.X) >= 0;
            }

            // use cross product to check if points is on line
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;

            // we don't need for dy 0 check here
            if (Math.Abs(dx / dy) != 1)
            {
                return false;
            }

            var dxP = point.X - p1.X;
            var dyP = point.Y - p1.Y;
            var cross = dxP * dy - dyP * dx;

            // point is on line if cross is 0
            if (cross != 0)
            {
                return false;
            }

            // now check if points is between p1 and p2
            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                return dx > 0 ?
                    p1.X <= point.X && point.X <= p2.X :
                    p2.X <= point.X && point.X <= p1.X;
            }

            return dy > 0 ?
                p1.Y <= point.Y && point.Y <= p2.Y :
                p2.Y <= point.Y && point.Y <= p1.Y;
        }
    }

    private readonly string[] _input;
    private readonly List<Line> _lines;
    private readonly string pattern = @"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)";
    private int _minX = int.MaxValue;
    private int _maxX = 0;
    private int _minY = int.MaxValue;
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

                _minX = Math.Min(_minX, Math.Min(p1.X, p2.X));
                _minY = Math.Min(_minY, Math.Min(p1.Y, p2.Y));

                _maxX = Math.Max(_maxX, Math.Max(p1.X, p2.X));
                _maxY = Math.Max(_maxY, Math.Max(p1.Y, p2.Y));

                return new Line(p1, p2);
            })
            .ToList();

        // Console.WriteLine($"min X = {_minX}, minY = {_minY}");
        // Console.WriteLine($"max X = {_maxX}, maxY = {_maxY}");
    }

    public override ValueTask<string> Solve_1()
    {
        var points = new List<Point>();

        Parallel.For(_minX, _maxX + 1, (x, _) =>
        {

            Parallel.For(_minY, _maxY + 1, (y, _) =>
            {
                var point = new Point(x, y);
                point.CoversLine = _lines.Count(l => l.IsPointOnLine(point));
                lock(points)
                {
                    points.Add(point);
                }
            });
        });

        var result = points.Count(p => p.CoversLine >= 2);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var points = new List<Point>();

        Parallel.For(_minX, _maxX + 1, (x, _) =>
        {

            Parallel.For(_minY, _maxY + 1, (y, _) =>
            {
                var point = new Point(x, y);
                point.CoversLine = _lines.Count(l => l.IsPointOnLine2(point));
                lock(points)
                {
                    points.Add(point);
                }
            });
        });

        var result = points.Count(p => p.CoversLine >= 2);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}