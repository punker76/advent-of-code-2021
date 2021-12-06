namespace AdventOfCode;

public class Day_06 : BaseDay
{
    [DebuggerDisplay("timer={timer}")]
    private class Fish
    {
        public int timer = -1;

        public Fish(int timer)
        {
            this.timer = timer;
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

    public override ValueTask<string> Solve_1()
    {
        var fishList = _input
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(t => new Fish(int.Parse(t)))
            .ToList();

        for (int i = 0; i < 80; i++)
        {
            var newFishList = new List<Fish>();
            foreach (var fish in fishList)
            {
                if (fish.CreateNewFish())
                {
                    newFishList.Add(new Fish(8));
                }
            }
            fishList.AddRange(newFishList);
        }

        var result = fishList.Count;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}