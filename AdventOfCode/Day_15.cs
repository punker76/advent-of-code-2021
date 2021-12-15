﻿namespace AdventOfCode;

public class Day_15 : BaseDay
{
    private class Risk
    {
        public int x, y;
        public int level;

        public Risk(int x, int y, int level)
        {
            this.x = x;
            this.y = y;
            this.level = level;
        }
    }

    private readonly string[] _input;
    private readonly Risk[][] _matrix;

    public Day_15()
    {
        // _input = new []
        // {
        //     "1163751742",
        //     "1381373672",
        //     "2136511328",
        //     "3694931569",
        //     "7463417111",
        //     "1319128137",
        //     "1359912421",
        //     "3125421639",
        //     "1293138521",
        //     "2311944581"
        // };
        _input = File.ReadAllLines(InputFilePath);

        _matrix = _input.Where(l => !string.IsNullOrEmpty(l)).Select((l, y) => l.ToCharArray().Select((o, x) => new Risk(x, y, int.Parse(o.ToString()))).ToArray()).ToArray();
    }

    private class CostComparer : IComparer<int>
    {
        public int Compare(int a, int b)
        {
            return b - a;
        }
    }

    // Dijkstra's algorithm with priority queue
    private int GetLowestTotalRisk(Risk[][] levels)
    {
        int maxX = levels[0].Length;
        int maxY = levels.Length;

        // direction arrays for simplification of getting neighbour
        int[] dx = {-1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        (int risk, bool visited) [, ] risks = new(int, bool) [maxY, maxX];

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                risks[y, x].risk = int.MaxValue;
                risks[y, x].visited = false;
            }
        }

        risks[0, 0].risk = 0;

        var queue = new PriorityQueue<Risk, int>();
        queue.Enqueue(new Risk(0, 0, 0), 0);

        while (queue.Count > 0)
        {
            var currRisk = queue.Dequeue();

            int x = currRisk.x;
            int y = currRisk.y;

            if (risks[y, x].visited) { continue; };
            risks[y, x].visited = true;

            for (int step = 0; step < 4; step++)
            {
                int nextX = x + dx[step];
                int nextY = y + dy[step];

                if (nextX >= 0 && nextX < maxX && nextY >= 0 && nextY < maxY)
                {
                    if (!risks[nextY, nextX].visited)
                    {
                        if (currRisk.level + levels[nextY][nextX].level < risks[nextY, nextX].risk)
                        {
                            risks[nextY, nextX].risk = currRisk.level + levels[nextY][nextX].level;

                            queue.Enqueue(new Risk(nextX, nextY, risks[nextY, nextX].risk), risks[nextY, nextX].risk);
                        }
                    }
                }
            }
        }

        return risks[maxY - 1, maxX - 1].risk;
    }

    public override ValueTask<string> Solve_1()
    {
        var result = GetLowestTotalRisk(_matrix);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}