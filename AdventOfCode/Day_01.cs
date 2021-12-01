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
        var increasedValues = GetIncreasedValues(_input.Select(x => Convert.ToInt32(x)).ToArray());

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {increasedValues}");
    }

    private int GetIncreasedValues(int[] input)
    {
        int increasedValues = 0;

        for (int i = 1; i < input.Length; i++)
        {
            var prevDepth = input[i - 1];
            var depth = input[i];
            if (depth > prevDepth)
            {
                increasedValues++;
            }
        }

        return increasedValues;
    }

    public override ValueTask<string> Solve_2()
    {
        var threeSums = new List<int>();

        for (int i = 0; i < _input.Length - 2; i++)
        {
            var threeSum = _input.Take(new Range(i, i + 3)).Select(x => Convert.ToInt32(x)).Sum();
            threeSums.Add(threeSum);
        }

        var increasedValues = GetIncreasedValues(threeSums.ToArray());

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {increasedValues}");
    }
}
