namespace AdventOfCode;

public class Day_02 : BaseDay
{
    private readonly string[] _input;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath).Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0f;

        var position = new Vector2(0, 0);

        foreach (var vector in _input.Select(x => GetInstructionVector(x.Split(' '))))
        {
            if (vector != default)
            {
                position += vector;
            }
        }

        result = position.X * position.Y;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    private Vector2 GetInstructionVector(string[] instruction)
    {
        if (instruction.Length != 2)
        {
            return default;
        }

        var command = instruction[0];
        var value = Convert.ToInt32(instruction[1]);

        return command switch
        {
            "forward" => new Vector2(value, 0),
            "down" => new Vector2(0, value),
            "up" => new Vector2(0, -value),
            _ => default
        };
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}
