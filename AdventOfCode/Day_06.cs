namespace AdventOfCode;

public class Day_06 : BaseDay
{
    [DebuggerDisplay("timer={timer}")]
    private class Fish
    {
        public int timer = -1;
        public long amount = -1; // how many fishes

        public Fish(int timer, long fishes)
        {
            this.timer = timer;
            this.amount = fishes;
        }

        public bool CreateNewFish()
        {
            var newFish = timer == 0;
            if (newFish)
            {
                timer = 6;
            }
            else
            {
                timer--;
            }
            return newFish;
        }
    }

    private readonly string _input;

    public Day_06()
    {
        // _input = "3,4,3,1,2";
        _input = File.ReadAllLines(InputFilePath) [0];
    }

    private long GetNewFishCount(List<Fish> input)
    {
        long newFishCount = 0;
        foreach (var fish in input)
        {
            if (fish.CreateNewFish())
            {
                newFishCount += fish.amount;
            }
        }
        return newFishCount;
    }

    private long GetFishCount(int days)
    {
        var fishList = _input
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(t => new Fish(int.Parse(t), 1))
            .ToList();

        for (int i = 0; i < days; i++)
        {
            var newFishes = GetNewFishCount(fishList);
            if (newFishes > 0)
            {
                fishList.Add(new Fish(8, newFishes));
            }
        }

        return fishList.Sum(f => f.amount);
    }

    public override ValueTask<string> Solve_1()
    {
        var result = GetFishCount(80);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = GetFishCount(256);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}