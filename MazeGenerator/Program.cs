using MazeGenerator;

internal class Program
{
    private static void Main(string[] args)
    {
        Thread.Sleep(5000);
        Console.Clear();

        var maze = new Maze(31, 21, () => new Cell());

        new MazeBuilder<Cell>(maze, new MazeGenerateOptions(0.25, 0.2))
            .Build();
        Thread.Sleep(500);
        var result = maze.FindPath(1, 1, GetEndPointCriteria(29, 19), wave => { wave.VizualizeWave(); Thread.Sleep(500); });
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