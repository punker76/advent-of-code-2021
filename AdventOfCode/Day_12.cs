namespace AdventOfCode;

public class Day_12 : BaseDay
{
    private class Cave
    {
        public string name;
        public HashSet<Cave> destinations = new();
        public int visits = 0;

        public bool IsSmall()
        {
            return name.ToLower() == name;
        }
    }

    private readonly string[] _input;
    private readonly ConcurrentDictionary<string, Cave> _caves = new ConcurrentDictionary<string, Cave>();

    public Day_12()
    {
        _input = new []
        {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"
        };
        // _input = new []
        // {
        //     "dc-end",
        //     "HN-start",
        //     "start-kj",
        //     "dc-start",
        //     "dc-HN",
        //     "LN-dc",
        //     "HN-end",
        //     "kj-sa",
        //     "kj-HN",
        //     "kj-dc"
        // };
        // _input = new []
        // {
        //     "fs-end",
        //     "he-DX",
        //     "fs-he",
        //     "start-DX",
        //     "pj-DX",
        //     "end-zg",
        //     "zg-sl",
        //     "zg-pj",
        //     "pj-he",
        //     "RW-he",
        //     "fs-DX",
        //     "pj-RW",
        //     "zg-RW",
        //     "start-pj",
        //     "he-WI",
        //     "zg-he",
        //     "pj-fs",
        //     "start-RW"
        // };

        // _input = File.ReadAllLines(InputFilePath);

        foreach (var line in _input)
        {
            var split = line.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var cave1 = _caves.GetOrAdd(split[0], new Cave() { name = split[0] });
            var cave2 = _caves.GetOrAdd(split[1], new Cave() { name = split[1] });

            cave1.destinations.Add(cave2);
            cave2.destinations.Add(cave1);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var paths = new List<List<Cave>>();

        var start = _caves.GetValueOrDefault("start", null);

        var stack = new Stack<Cave[]>();
        stack.Push(new [] { start });

        void walkthrough(Cave[] path)
        {
            var last = path.LastOrDefault();
            if (last is not null)
            {
                last.destinations.ForEach(next =>
                {
                    if (next.name == "end")
                    {
                        var endPath = new List<Cave>(path);
                        endPath.Add(next);
                        paths.Add(endPath);
                    }
                    else if (!next.IsSmall() || !path.Contains(next))
                    {
                        var newPath = new List<Cave>(path);
                        newPath.Add(next);
                        stack.Push(newPath.ToArray());
                    }
                });
            }
        };

        while (stack.Count > 0)
        {
            walkthrough(stack.Pop());
        }

        var result = paths.Count;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var paths = new List<List<Cave>>();

        var start = _caves.GetValueOrDefault("start", null);

        var stack = new Stack<Cave[]>();
        stack.Push(new [] { start });

        void walkthrough(Cave[] path)
        {
            var last = path.LastOrDefault();
            if (last is not null)
            {
                last.destinations.ForEach(next =>
                {
                    if (next.name == "end")
                    {
                        var endPath = new List<Cave>(path);
                        endPath.Add(next);
                        paths.Add(endPath);
                    }
                    else if (!next.IsSmall() || !path.Contains(next))
                    {
                        var newPath = new List<Cave>(path);
                        newPath.Add(next);
                        stack.Push(newPath.ToArray());
                    }
                    else if (next.name != "start" && next.IsSmall() && path.Count(c => c == next) == 1)
                    {
                        var newPath = new List<Cave>(path);
                        newPath.Add(next);
                        stack.Push(newPath.ToArray());
                    }
                });
            }
        };

        while (stack.Count > 0)
        {
            walkthrough(stack.Pop());
        }

        var result = paths.Count;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}