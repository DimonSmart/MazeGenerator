using MazeGenerator;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();

        var maze = new Maze(20, 11, () => new Cell());

        new MazeBuilder<Cell>(maze, new MazeGenerateOptions(0.75, 0.0))
            .Build();

        maze.FindPath(1, 1, GetEndPointCriteria(9, 9), wave => { wave.Redraw(); Thread.Sleep(100); });
    }


    // In case we need to find a big object, not a point.
    private static Func<int, int, bool> GetEndPointCriteria(int endX, int endY)
    {
        return (x, y) => x == endX && y == endY;
    }
}