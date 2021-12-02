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
        var result = 0L;

        var position = (horizontal: 0L, depth: 0L);

        foreach (var instruction in _input.Select(x => GetInstructionVector(x.Split(' '))))
        {
            position.horizontal += instruction.horizontal;
            position.depth += instruction.depth;
        }

        result = position.horizontal * position.depth;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    private (long horizontal, long depth) GetInstructionVector(string[] instruction)
    {
        if (instruction.Length != 2)
        {
            return default;
        }

        var command = instruction[0];
        var value = Convert.ToInt64(instruction[1]);

        return command switch
        {
            "forward" => (value, 0),
            "down" => (0, value),
            "up" => (0, -value),
            _ => default
        };
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0L;

        var position = (horizontal: 0L, depth: 0L);

        foreach (var instruction in _input.Select(x => GetInstructionVector(x.Split(' '))))
        {
            position.horizontal += instruction.horizontal;
            position.depth += instruction.depth;
            result += instruction.horizontal * position.depth;
        }

        result = position.horizontal * result;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}
