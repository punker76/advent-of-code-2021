namespace AdventOfCode;

public class Day_11 : BaseDay
{
    private class DumboOcto
    {
        public int x, y;
        public int energyLevel;

        public DumboOcto(int x, int y, int level)
        {
            this.x = x;
            this.y = y;
            this.energyLevel = level;
        }
    }

    private readonly DumboOcto[][] _input;

    public Day_11()
    {
        // _input = new []
        //     {
        //         "5483143223",
        //         "2745854711",
        //         "5264556173",
        //         "6141336146",
        //         "6357385478",
        //         "4167524645",
        //         "2176841721",
        //         "6882881134",
        //         "4846848554",
        //         "5283751526"
        //     }
        //     .Select((l, y) => l.ToCharArray().Select((o, x) => new DumboOcto(x, y, int.Parse(o.ToString()))).ToArray())
        //     .ToArray();

        _input = File.ReadAllLines(InputFilePath)
            .Select((l, y) => l.ToCharArray().Select((o, x) => new DumboOcto(x, y, int.Parse(o.ToString()))).ToArray())
            .ToArray();
    }

    private void PrintGrid(DumboOcto[][] grid)
    {
        foreach (var line in grid)
        {
            foreach (var octo in line)
            {
                // Console.Write($"{octo.x},{octo.y}: {octo.energyLevel}  ");
                Console.Write($"{octo.energyLevel}  ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private IEnumerable<DumboOcto> GetOctoAround(DumboOcto currentOcto, DumboOcto[][] grid)
    {
        for (var stepX = -1; stepX < 2; stepX++)
        {
            for (var stepY = -1; stepY < 2; stepY++)
            {
                if (stepX != 0 || stepY != 0)
                {
                    var x = currentOcto.x + stepX;
                    var y = currentOcto.y + stepY;

                    if (x >= 0 && y >= 0 && x < grid[0].Count() && y < grid.Count())
                    {
                        var o = grid[y][x];
                        yield return o;
                    }
                }
            }
        }
    }

    private void Flash(DumboOcto octo, HashSet<DumboOcto> flashedOctos, DumboOcto[][] grid)
    {
        if (!flashedOctos.Contains(octo))
        {
            flashedOctos.Add(octo);

            var octosAround = GetOctoAround(octo, grid).Where(o => o.energyLevel < 10).ToList();

            foreach (var o in octosAround)
            {
                o.energyLevel++;

                if (o.energyLevel == 10)
                {
                    Flash(o, flashedOctos, grid);
                }
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0;

        for (int i = 0; i < 100; i++)
        {
            var flashedOctos = new HashSet<DumboOcto>();

            foreach (var octo in _input.SelectMany(o => o))
            {
                octo.energyLevel++;
            }

            foreach (var octo in _input.SelectMany(o => o).Where(o => o.energyLevel > 9))
            {
                Flash(octo, flashedOctos, _input);
            }

            foreach (var octo in _input.SelectMany(o => o).Where(o => o.energyLevel > 9))
            {
                octo.energyLevel = 0;
            }

            result += flashedOctos.Count();
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}