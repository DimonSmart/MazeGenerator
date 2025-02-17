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

        new MazeBuilder<Cell>(maze, new MazeBuildOptions(0.1, 0.0)).Build(mazePlotter);

        var wave = new MazeWaveGenerator<Cell>(maze, mazePlotter).GenerateWave(1, 1, 29, 19);
        var pathBuilder = new MazePathBuilder(wave, mazePlotter);
        pathBuilder.BuildPath();

        Console.CursorVisible = true;
        Thread.Sleep(10000);
    }
}