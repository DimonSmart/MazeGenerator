using DimonSmart.MazeGenerator;
using DimonSmart.MazeGeneratorConsoleDemo;
using MazeGeneratorConsoleDemo;

internal class Program
{
    private static void Main(string[] _)
    {
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        var mazePlotter = new MazeConsolePlotter(TimeSpan.FromMilliseconds(25));

        var maze = new Maze<Cell>(31, 21);

        new MazeBuilder(maze, new MazeBuildOptions(0.50, 0.0)).Build(mazePlotter);

        var result = maze.FindPath(1, 1, GetEndPointCriteria(29, 19), wave => { wave.VisualizeWave(mazePlotter); Thread.Sleep(500); });
        result?.VisualizePath(mazePlotter);

        Console.CursorVisible = true;
        Thread.Sleep(10000);
    }

    // In case we need to find a big object, not a point.
    private static Func<int, int, bool> GetEndPointCriteria(int endX, int endY)
    {
        return (x, y) => x == endX && y == endY;
    }
}