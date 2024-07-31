using DimonSmart.MazeGenerator;
using DimonSmart.MazeGeneratorConsoleDemo;
using MazeGeneratorConsoleDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        var mazePlotter = new MazeConsolePlotter(TimeSpan.FromMilliseconds(25));

        var maze = new Maze(31, 21, () => new Cell(), mazePlotter);

        new MazeBuilder<Cell>(maze, new MazeGenerateOptions(0.50, 0.0))
            .Build();

        var result = maze.FindPath(1, 1, GetEndPointCriteria(29, 19), wave => { wave.VisualizeWave(mazePlotter); Thread.Sleep(500); });
        if (result != null)
        {
            result.VizualizePath(mazePlotter);
        }

        Console.CursorVisible = true;
        Thread.Sleep(10000);
    }

    // In case we need to find a big object, not a point.
    private static Func<int, int, bool> GetEndPointCriteria(int endX, int endY)
    {
        return (x, y) => x == endX && y == endY;
    }
}