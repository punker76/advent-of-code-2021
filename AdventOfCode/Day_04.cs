using Spectre.Console;

namespace AdventOfCode;

public class Day_04 : BaseDay
{
    private class BoardNumber
    {
        public int number = 0;
        public bool found = false;

        public BoardNumber(int number)
        {
            this.number = number;
        }
    }

    private readonly string[] _input;
    private readonly int[] _numbers;
    private readonly List<BoardNumber[][]> _boards = new();

    public Day_04()
    {
        _input = File.ReadAllLines(InputFilePath);

        _numbers = _input[0]
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse).ToArray();

        foreach (var chunk in _input.Skip(2).Chunk(6))
        {
            var board = chunk.Take(5)
                .Select(l => l
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(i => new BoardNumber(int.Parse(i)))
                    .ToArray())
                .ToArray();
            _boards.Add(board);
        }
    }

    private void PrintBoard(BoardNumber[][] board)
    {
        foreach (var line in board)
        {
            foreach (var boardNumber in line)
            {
                Console.Write($"{boardNumber.number} : {boardNumber.found} ");
            }

            Console.WriteLine();
        }
    }

    private int GetUnmarkedBoardSum(BoardNumber[][] board)
    {
        return board.Sum(line => line.Where(boardNumber => !boardNumber.found).Sum(boardNumber => boardNumber.number));
    }

    private bool BoardWins(BoardNumber[][] board, int number)
    {
        foreach (var row in board)
        {
            foreach (var (i, boardNumber) in row.Select(((item, index) => (index, item))))
            {
                if (boardNumber.number == number)
                {
                    boardNumber.found = true;
                }

                var colWins = board.Select(x => x[i]).All(x => x.found);
                if (colWins)
                {
                    return true;
                }
            }

            var rowWins = row.All(x => x.found);
            if (rowWins)
            {
                return true;
            }
        }

        return false;
    }

    private (int number, BoardNumber[][] board) GetWinnerBoard()
    {
        foreach (var number in _numbers)
        {
            foreach (var board in _boards)
            {
                if (BoardWins(board, number))
                {
                    return (number, board);
                }
            }
        }

        return default;
    }

    private (int number, BoardNumber[][] board) GetLastBoard()
    {
        var winnerBoards = new HashSet<BoardNumber[][]>();

        foreach (var number in _numbers)
        {
            foreach (var board in _boards.Where(board => BoardWins(board, number)))
            {
                winnerBoards.Add(board);

                if (winnerBoards.Count == _boards.Count)
                {
                    return (number, board);
                }
            }
        }

        return default;
    }

    public override ValueTask<string> Solve_1()
    {
        var (number, board) = GetWinnerBoard();

#if DEBUG
        PrintBoard(board);
#endif

        var result = number * GetUnmarkedBoardSum(board);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {result}");
    }

    public override ValueTask<string> Solve_2()
    {
        var (number, board) = GetLastBoard();

#if DEBUG
        PrintBoard(board);
#endif

        var result = number * GetUnmarkedBoardSum(board);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {result}");
    }
}