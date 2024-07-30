using MazeGenerator;
using MazeGeneratorConsoleDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        var wavePlotter = new WaveConsolePlotter();

        var maze = new Maze(31, 21, () => new Cell());
       
        new MazeBuilder<Cell>(maze, new MazeGenerateOptions(0.50, 0.0))
            .Build();
        maze.Redraw();

        var result = maze.FindPath(1, 1, GetEndPointCriteria(29, 19), wave => { wave.VisualizeWave(wavePlotter); Thread.Sleep(500); });
        if (result.HasValue)
        {
            result.Value.VizualizePath();
        }


        Thread.Sleep(10000);
    }


    // In case we need to find a big object, not a point.
    private static Func<int, int, bool> GetEndPointCriteria(int endX, int endY)
    {
        return (x, y) => x == endX && y == endY;
    }
}