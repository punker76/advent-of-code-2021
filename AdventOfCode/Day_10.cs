namespace AdventOfCode;

public class Day_10 : BaseDay
{
    private readonly char[][] _input;
    private readonly List<char[]> _incomplete = new();
    private readonly char[] openTags = new [] { '(', '[', '{', '<' };
    private readonly char[] closeTags = new [] { ')', ']', '}', '>' };
    private readonly int[] syntaxErrorPoints = new [] { 3, 57, 1197, 25137 };
    private readonly int[] scorePoints = new [] { 1, 2, 3, 4 };

    public Day_10()
    {
        // _input = new []
        //     {
        //         "[({(<(())[]>[[{[]{<()<>>",
        //         "[(()[<>])]({[<{<<[]>>(",
        //         "{([(<{}[<>[]}>{[]{[(<()>",
        //         "(((({<>}<{<{<>}{[]{[]{}",
        //         "[[<[([]))<([[{}[[()]]]",
        //         "[{[{({}]{}}([{[{{{}}([]",
        //         "{<[[]]>}<{[{[{[]{()[[[]",
        //         "[<(<(<(<{}))><([]([]()",
        //         "<{([([[(<>()){}]>(<<{{",
        //         "<{([{{}}[<[[[<>{}]]]>[]]"
        //     }
        //     .Select(l => l.ToCharArray())
        //     .ToArray();

        _input = File.ReadAllLines(InputFilePath)
            .Select(l => l.ToCharArray())
            .ToArray();

        _incomplete.AddRange(_input);
    }

    public override ValueTask<string> Solve_1()
    {
        int[] resultArray = new [] { 0, 0, 0, 0 };

        foreach (var line in _input)
        {
            var openStack = new Stack<char>();

            foreach (var c in line)
            {
                if (openTags.Contains(c))
                {
                    openStack.Push(c);
                }
                else if (closeTags.Contains(c))
                {
                    var index = Array.IndexOf(closeTags, c);
                    var openTag = openTags[index];
                    var lastOpenTag = openStack.Pop();
                    if (lastOpenTag != openTag)
                    {
                        // invalid
                        resultArray[index] += syntaxErrorPoints[index];
                        _incomplete.Remove(line);
                        continue;
                    }
                }
            }
        }

        var result = resultArray.Sum();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        List<long> scores = new();

        foreach (var line in _incomplete)
        {
            var openStack = new Stack<char>();

            foreach (var c in line)
            {
                if (openTags.Contains(c))
                {
                    openStack.Push(c);
                }
                else if (closeTags.Contains(c))
                {
                    openStack.Pop();
                }
            }

            var totalScore = 0L;
            while (openStack.Count > 0)
            {
                var index = Array.IndexOf(openTags, openStack.Pop());
                var closeTag = closeTags[index];
                totalScore = totalScore * 5 + scorePoints[index];
            }
            scores.Add(totalScore);
        }

        var result = scores.OrderBy(s => s).ElementAtOrDefault(scores.Count / 2);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}