namespace AdventOfCode;

public class Day_10 : BaseDay
{
    private readonly char[][] _input;
    private readonly char[] openTags = new [] { '(', '[', '{', '<' };
    private readonly char[] closeTags = new [] { ')', ']', '}', '>' };
    private readonly int[] incorrectPoints = new [] { 3, 57, 1197, 25137 };

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
                        resultArray[index] += incorrectPoints[index];
                    }
                }
            }
        }

        var result = resultArray.Sum();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}