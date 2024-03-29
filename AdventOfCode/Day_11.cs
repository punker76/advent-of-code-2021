﻿namespace AdventOfCode;

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

    private readonly string[] _input;

    public Day_11()
    {
        // _input = new []
        // {
        //     "5483143223",
        //     "2745854711",
        //     "5264556173",
        //     "6141336146",
        //     "6357385478",
        //     "4167524645",
        //     "2176841721",
        //     "6882881134",
        //     "4846848554",
        //     "5283751526"
        // };

        _input = File.ReadAllLines(InputFilePath);
    }

    private void PrintGrid(DumboOcto[][] grid)
    {
        foreach (var line in grid)
        {
            foreach (var octo in line)
            {
                Console.Write($"{octo.energyLevel}");
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
                        yield return grid[y][x];
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

            GetOctoAround(octo, grid).Where(o => o.energyLevel < 10).ForEach(o =>
            {
                o.energyLevel++;

                if (o.energyLevel == 10)
                {
                    Flash(o, flashedOctos, grid);
                }
            });
        }
    }

    private int GetFlashedOctos(DumboOcto[][] grid)
    {
        var flashedOctos = new HashSet<DumboOcto>();

        grid.SelectMany(o => o).ForEach(o => { o.energyLevel++; });
        grid.SelectMany(o => o).Where(o => o.energyLevel > 9).ForEach(o => Flash(o, flashedOctos, grid));
        grid.SelectMany(o => o).Where(o => o.energyLevel > 9).ForEach(o => { o.energyLevel = 0; });

        return flashedOctos.Count();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0;

        var octos = _input.Select((l, y) => l.ToCharArray().Select((o, x) => new DumboOcto(x, y, int.Parse(o.ToString()))).ToArray()).ToArray();

        for (int i = 0; i < 100; i++)
        {
            result += GetFlashedOctos(octos);
        }

        // PrintGrid(octos);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        var octos = _input.Select((l, y) => l.ToCharArray().Select((o, x) => new DumboOcto(x, y, int.Parse(o.ToString()))).ToArray()).ToArray();

        while (true)
        {
            ++result;
            GetFlashedOctos(octos);
            if (octos.SelectMany(o => o).All(o => o.energyLevel == 0))
            {
                break;
            }
        }

        // PrintGrid(octos);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}