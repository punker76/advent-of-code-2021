namespace AdventOfCode;

public class Day_09 : BaseDay
{
    private class Point
    {
        public int x;
        public int y;
        public int height = 0;
        public bool lowest = false;
        public int basinSize = 0;

        public Point(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.height = value;
        }
    }

    private readonly string[] _input;
    private readonly Point[][] _heightmap;

    public Day_09()
    {
        // _input = new []
        // {
        //     "2199943210",
        //     "3987894921",
        //     "9856789892",
        //     "8767896789",
        //     "9899965678"
        // };
        _input = File.ReadAllLines(InputFilePath);

        _heightmap = _input
            .Select((l, y) => l.ToCharArray().Select((c, x) => new Point(x, y, int.Parse((c.ToString())))).ToArray())
            .ToArray();
    }

    private void PrintHeightmap(Point[][] map)
    {
        foreach (var line in map)
        {
            foreach (var point in line)
            {
                Console.Write($"{point.height}:{(point.lowest ? "1" : "0")}  ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private IEnumerable<Point> GetPointsAround(Point point, Point[][] map)
    {
        // horizontal
        for (var x = -1; x < 2; x++)
        {
            if (x != 0)
            {
                var p = map[point.y].ElementAtOrDefault(point.x + x);
                if (p != null)
                {
                    yield return p;
                }
            }
        }

        // vertical
        for (var y = -1; y < 2; y++)
        {
            if (y != 0)
            {
                var p = map.ElementAtOrDefault(point.y + y);
                if (p != null)
                {
                    yield return p[point.x];
                }
            }
        }
    }

    private bool IsLowestPoint(Point point, Point[][] map)
    {
        var points = GetPointsAround(point, map);
        return point.height < points.Min(p => p.height);
    }

    private Point[][] MarkLowestOnHeightmap(Point[][] map)
    {
        foreach (var row in map)
        {
            foreach (var point in row)
            {
                point.lowest = IsLowestPoint(point, map);
            }
        }

        return map;
    }

    private IEnumerable<Point> GetPointsAround2(Point point, Point[][] map)
    {
        var points = new HashSet<Point>();
        foreach (var p in GetPointsAround(point, map).Where(p => p.height > point.height && p.height < 9))
        {
            points.Add(p);
            foreach (var pp in GetPointsAround2(p, map))
            {
                points.Add(pp);
            }
        }
        return points;
    }

    private Point[][] MarkLowestOnHeightmap2(Point[][] map)
    {
        foreach (var row in map)
        {
            foreach (var point in row)
            {
                point.lowest = IsLowestPoint(point, map);
                if (point.lowest)
                {
                    var points = GetPointsAround2(point, map);
                    point.basinSize = points.Count() + 1;
                }
            }
        }

        return map;
    }

    public override ValueTask<string> Solve_1()
    {
        var map = MarkLowestOnHeightmap(_heightmap.Clone() as Point[][]);
        var result = map
            .SelectMany(row => row.Where(p => p.lowest))
            .Sum(p => p.height + 1);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var map = MarkLowestOnHeightmap2(_heightmap.Clone() as Point[][]);
        var result = map
            .SelectMany(row => row.Where(p => p.lowest))
            .OrderByDescending(p => p.basinSize)
            .Take(3)
            .Aggregate(1, (r, p) => r * p.basinSize);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}