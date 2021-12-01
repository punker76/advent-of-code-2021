namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string[] _input;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath).Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }

    public override ValueTask<string> Solve_1()
    {
        // int? prevMeasurement = null;
        int increasedValues = 0;

        for (int i = 1; i < _input.Length; i++)
        {
            var prevDepth = Convert.ToInt32(_input[i - 1]);
            var depth = Convert.ToInt32(_input[i]);
            if (depth > prevDepth)
            {
                increasedValues++;
            }
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {increasedValues}");
    }

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}
