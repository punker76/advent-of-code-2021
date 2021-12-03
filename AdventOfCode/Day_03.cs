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
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}
