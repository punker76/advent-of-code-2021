namespace AdventOfCode;

public class Day_07 : BaseDay
{
    private class Crab
    {
        public int position;

        public Crab(int position)
        {
            this.position = position;
        }

        public int GetFuelForPosition(int toPos)
        {
            return Math.Abs(position - toPos);
        }

        public int GetFuelForPosition2(int toPos)
        {
            var fuel = GetFuelForPosition(toPos);
            return fuel * (fuel + 1) / 2;
        }
    }

    private readonly string _input;
    private readonly List<Crab> _crabs;

    public Day_07()
    {
        // _input = "16,1,2,0,4,2,7,1,2,14";
        _input = File.ReadAllLines(InputFilePath) [0];
        _crabs = _input
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(p => new Crab(int.Parse(p)))
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var maxPosition = _crabs.Max(c => c.position);
        var lowestFuel = (pos: 0, fuel: int.MaxValue);

        for (int pos = 1; pos <= maxPosition; pos++)
        {
            var fuel = _crabs.Sum(c => c.GetFuelForPosition(pos));
            if (fuel < lowestFuel.fuel)
            {
                lowestFuel.fuel = fuel;
                lowestFuel.pos = pos;
            }
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {lowestFuel.fuel} on {lowestFuel.pos}");
    }

    public override ValueTask<string> Solve_2()
    {
        var maxPosition = _crabs.Max(c => c.position);
        var lowestFuel = (pos: 0, fuel: int.MaxValue);

        for (int pos = 1; pos <= maxPosition; pos++)
        {
            var fuel = _crabs.Sum(c => c.GetFuelForPosition2(pos));
            if (fuel < lowestFuel.fuel)
            {
                lowestFuel.fuel = fuel;
                lowestFuel.pos = pos;
            }
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {lowestFuel.fuel} on {lowestFuel.pos}");
    }
}