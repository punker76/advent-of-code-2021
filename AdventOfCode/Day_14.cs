namespace AdventOfCode;

public class Day_14 : BaseDay
{
    private readonly string[] _input;
    private readonly string _polymerTemplate;
    private readonly Dictionary<string, string> _rules;

    public Day_14()
    {
        // _input = new []
        // {
        //     "NNCB",
        //     "",
        //     "CH -> B",
        //     "HH -> N",
        //     "CB -> H",
        //     "NH -> C",
        //     "HB -> C",
        //     "HC -> B",
        //     "HN -> C",
        //     "NN -> C",
        //     "BH -> H",
        //     "NC -> B",
        //     "NB -> B",
        //     "BN -> B",
        //     "BB -> N",
        //     "BC -> B",
        //     "CC -> N",
        //     "CN -> C"
        // };
        _input = File.ReadAllLines(InputFilePath);

        _polymerTemplate = _input[0];

        _rules = _input
            .Skip(2)
            .Select(l =>
            {
                var rule = l.Split(" -> ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return (rule[0], rule[0][0] + rule[1] + rule[0][1]);
            })
            .ToDictionary(x => x.Item1, x => x.Item2);
    }

    public StringBuilder Process(StringBuilder template)
    {
        var polymer = new StringBuilder();
        polymer.Append(template[0]);
        for (int i = 0; i < template.Length - 1; i++)
        {
            var pair = $"{template[i]}{template[i+1]}";
            if (_rules.TryGetValue(pair, out var result))
            {
                polymer.Remove(polymer.Length - 1, 1);
                polymer.Append(result);
            }
        }
        return polymer;
    }

    public override ValueTask<string> Solve_1()
    {
        var template = new StringBuilder(_polymerTemplate);

        for (int step = 1; step <= 10; step++)
        {
            template = Process(template);

        }

        var grouped = template.ToString().ToCharArray().GroupBy(c => c).OrderBy(g => g.Count());

        var result = grouped.Last().Count() - grouped.First().Count();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}