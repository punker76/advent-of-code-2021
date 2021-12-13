namespace AdventOfCode;

public class Day_13 : BaseDay
{
    // [DebuggerDisplay("{name}")]

    private readonly string[] _input;

    private readonly bool[, ] _sheet;

    private readonly List < (string axis, int value) > _instructions = new();

    public Day_13()
    {
        // _input = new []
        // {
        //     "6,10",
        //     "0,14",
        //     "9,10",
        //     "0,3",
        //     "10,4",
        //     "4,11",
        //     "6,0",
        //     "6,12",
        //     "4,1",
        //     "0,13",
        //     "10,12",
        //     "3,4",
        //     "3,0",
        //     "8,4",
        //     "1,10",
        //     "2,14",
        //     "8,10",
        //     "9,0",
        //     "",
        //     "fold along y=7",
        //     "fold along x=5"
        // };
        _input = File.ReadAllLines(InputFilePath);

        var points = new List < (int x, int y) > ();

        foreach (var line in _input)
        {
            if (line.Contains(','))
            {
                var split = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var x = int.Parse(split[0]);
                var y = int.Parse(split[1]);
                points.Add((x, y));
            }
            else if (line.Contains('='))
            {
                var split = line.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var foldAxis = split[0].Replace("fold along ", "");
                var fold = int.Parse(split[1]);
                _instructions.Add((foldAxis, fold));
            }
        }

        var maxX = points.Max(p => p.x) + 1;
        var maxY = points.Max(p => p.y) + 1;

        _sheet = new bool[maxY, maxX];
        foreach (var p in points)
        {
            _sheet[p.y, p.x] = true;
        }
    }

    public int GetVisiblePoints(bool[, ] sheet)
    {
        var result = 0;
        for (int y = 0; y < sheet.GetLength(0); y++)
        {
            for (int x = 0; x < sheet.GetLength(1); x++)
            {
                if (sheet[y, x])
                {
                    result++;
                }
            }
        }
        return result;
    }

    public bool[, ] FoldHorizontal(bool[, ] sheet, int foldAt)
    {
        var my = sheet.GetLength(0);
        var mx = sheet.GetLength(1);

        var newSheet = new bool[foldAt, mx];

        for (int y = 0; y < foldAt; y++)
        {
            for (int x = 0; x < mx; x++)
            {
                newSheet[y, x] = sheet[y, x];
            }
        }

        for ((int y1, int y2) = (y1 = foldAt - 1, y2 = foldAt + 1); y1 >= 0 && y2 < my; y1--, y2++)
        {
            for (int x = 0; x < mx; x++)
            {
                newSheet[y1, x] = newSheet[y1, x] || sheet[y2, x];
            }
        }

        return newSheet;
    }

    public bool[, ] FoldVertical(bool[, ] sheet, int foldAt)
    {
        var my = sheet.GetLength(0);
        var mx = sheet.GetLength(1);

        var newSheet = new bool[my, foldAt];

        for (int x = 0; x < foldAt; x++)
        {
            for (int y = 0; y < my; y++)
            {
                newSheet[y, x] = sheet[y, x];
            }
        }

        for ((int x1, int x2) = (x1 = foldAt - 1, x2 = foldAt + 1); x1 >= 0 && x2 < mx; x1--, x2++)
        {
            for (int y = 0; y < my; y++)
            {
                newSheet[y, x1] = newSheet[y, x1] || sheet[y, x2];
            }
        }

        return newSheet;
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0;

        (string axis, int value) firstI = _instructions.First();

        if (firstI.axis == "x")
        {
            // fold the paper left

            var newSheet = FoldVertical(_sheet, firstI.value);
            result = GetVisiblePoints(newSheet);
        }
        else if (firstI.axis == "y")
        {
            // fold the paper up

            var newSheet = FoldHorizontal(_sheet, firstI.value);
            result = GetVisiblePoints(newSheet);
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}