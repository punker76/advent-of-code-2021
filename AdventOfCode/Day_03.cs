namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string[] _input;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath).Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    public override ValueTask<string> Solve_1()
    {
        var matrix = _input.Select(x => x.ToCharArray()).ToArray();

        var gamma = "";
        var eps = "";

        for (int col = 0; col < matrix[0].Length; col++)
        {
            var sum = matrix.Select(x => x[col] == '1' ? 1 : 0).Sum();
            if (sum > matrix.Length / 2)
            {
                gamma += '1';
                eps += '0';
            }
            else
            {
                gamma += '0';
                eps += '1';
            }
        }

        var result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(eps, 2);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var oxygen = _input.Select(x => x.ToCharArray()).ToArray();
        var co2 = _input.Select(x => x.ToCharArray()).ToArray();

        var colLength = oxygen[0].Length;

        for (int col = 0; col < colLength; col++)
        {
            var zeroCount = oxygen.Count(x => x[col] == '0');
            var nonZeroCount = oxygen.Count(x => x[col] == '1');
            if (zeroCount < nonZeroCount || zeroCount == nonZeroCount)
            {
                oxygen = oxygen.Where(x => x[col] == '1').ToArray();
            }
            else
            {
                oxygen = oxygen.Where(x => x[col] == '0').ToArray();
            }

            if (co2.Length > 1)
            {
                zeroCount = co2.Count(x => x[col] == '0');
                nonZeroCount = co2.Count(x => x[col] == '1');
                if (zeroCount < nonZeroCount || zeroCount == nonZeroCount)
                {
                    co2 = co2.Where(x => x[col] == '0').ToArray();
                }
                else
                {
                    co2 = co2.Where(x => x[col] == '1').ToArray();
                }
            }
        }

        var result = Convert.ToInt32(new string(oxygen[0]), 2) * Convert.ToInt32(new string(co2[0]), 2);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}
