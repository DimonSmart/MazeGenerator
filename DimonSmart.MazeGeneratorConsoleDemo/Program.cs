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

        var maze = new Maze<Cell>(31, 21);
        var mazePlotter = new MazeConsolePlotter();

        new MazeBuilder(maze, new MazeBuildOptions(0.50, 0.0)).Build(mazePlotter);

        var result = new MazePathFinder(maze, mazePlotter).FindPath(1, 1, 29, 19);
        result.VisualizePath(mazePlotter);

        Console.CursorVisible = true;
        Thread.Sleep(10000);
    }
}