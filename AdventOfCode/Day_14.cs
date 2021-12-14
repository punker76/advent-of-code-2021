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
        //     "CN -> C",
        //     ""
        // };
        _input = File.ReadAllLines(InputFilePath);

        _polymerTemplate = _input[0];

        _rules = _input
            .Skip(2)
            .Select(l =>
            {
                var rule = l.Split(" -> ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return (rule[0], rule[1]);
            })
            .ToDictionary(x => x.Item1, x => x.Item2);
    }

    private StringBuilder Process(StringBuilder template)
    {
        var polymer = new StringBuilder();
        polymer.Append(template[0]);
        for (int i = 0; i < template.Length - 1; i++)
        {
            var pair = $"{template[i]}{template[i+1]}";
            if (_rules.TryGetValue(pair, out var rule))
            {
                polymer.Append($"{rule}{template[i+1]}");
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

    private void AddPairs(Dictionary<string, long> pairs, string pair, long count)
    {
        if (pairs.ContainsKey(pair))
        {
            pairs[pair] += count;
        }
        else
        {
            pairs.Add(pair, count);
        }
    }

    public override ValueTask<string> Solve_2()
    {
        Dictionary<string, long> chars = new();
        for (int i = 0; i < _polymerTemplate.Length; i++)
        {
            AddPairs(chars, _polymerTemplate[i].ToString(), 1);
        }

        Dictionary<string, long> pairs = new();
        for (int i = 0; i < _polymerTemplate.Length - 1; i++)
        {
            var pair = $"{_polymerTemplate[i]}{_polymerTemplate[i+1]}";
            AddPairs(pairs, pair, 1);
        }

        for (int step = 1; step <= 40; step++)
        {
            Dictionary<string, long> pairs2 = new();
            foreach (var pair in pairs)
            {
                if (_rules.TryGetValue(pair.Key, out var rule))
                {
                    AddPairs(chars, rule, pair.Value);
                    AddPairs(pairs2, $"{pair.Key[0]}{rule}", pair.Value);
                    AddPairs(pairs2, $"{rule}{pair.Key[1]}", pair.Value);
                }
            }
            pairs = pairs2;
        }

        var result = (chars.Values.Max() - chars.Values.Min());

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}