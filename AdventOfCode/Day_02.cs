namespace AdventOfCode;

public struct VectorL : IEquatable<VectorL>
{
    public long X;
    public long Y;

    public VectorL(long x, long y)
    {
        X = x;
        Y = y;
    }

    public static VectorL operator +(VectorL left, VectorL right)
    {
        return new VectorL(
            left.X + right.X,
            left.Y + right.Y
        );
    }

    public static bool operator ==(VectorL left, VectorL right)
    {
        return (left.X == right.X)
            && (left.Y == right.Y);
    }

    public static bool operator !=(VectorL left, VectorL right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        return obj is VectorL other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public bool Equals(VectorL other)
    {
        return this == other;
    }
}

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

        var position = new VectorL(0, 0);

        foreach (var vector in _input.Select(x => GetInstructionVector(x.Split(' '))))
        {
            if (vector != default)
            {
                position += vector;
            }
        }

        result = Convert.ToInt64(position.X * position.Y);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    private VectorL GetInstructionVector(string[] instruction)
    {
        if (instruction.Length != 2)
        {
            return default;
        }

        var command = instruction[0];
        var value = Convert.ToInt32(instruction[1]);

        return command switch
        {
            "forward" => new VectorL(value, 0),
            "down" => new VectorL(0, value),
            "up" => new VectorL(0, -value),
            _ => default
        };
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0L;

        var position = new VectorL(0, 0);

        foreach (var vector in _input.Select(x => GetInstructionVector(x.Split(' '))))
        {
            if (vector != default)
            {
                position += vector;
                result += Convert.ToInt64(vector.X * position.Y);
            }
        }

        result = Convert.ToInt64(position.X * result);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}
