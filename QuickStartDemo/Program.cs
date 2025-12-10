using System.Collections.Generic;
using System.Linq;
using System.Text;
using DimonSmart.MazeGenerator;

Console.OutputEncoding = Encoding.UTF8;

RunBasicGeneration();
RunCustomPlotter();
RunWavePathfinding();

static void RunBasicGeneration()
{
    Console.WriteLine("=== Example 1: minimal generation (defaults) ===");
    const int size = 9;
    var maze = new Maze<TutorialCell>(size, size);
    new MazeBuilder<TutorialCell>(maze).Build();
    RenderMaze(maze);
    Console.WriteLine();
}

static void RunCustomPlotter()
{
    Console.WriteLine("=== Example 2: custom options + custom plotter ===");
    const int size = 9;
    var maze = new Maze<TutorialCell>(size, size);
    var options = new MazeBuildOptions(WallShortness: 0.35, Emptiness: 0.2);
    var plotter = new SnapshotPlotter(size, size);
    new MazeBuilder<TutorialCell>(maze, options).Build(plotter);
    Console.WriteLine(plotter.Render());
}

static void RunWavePathfinding()
{
    Console.WriteLine("=== Example 3: wave propagation + path reconstruction ===");
    const int size = 9;
    var maze = new Maze<TutorialCell>(size, size);
    var options = new MazeBuildOptions(WallShortness: 0.15, Emptiness: 0.05);
    new MazeBuilder<TutorialCell>(maze, options).Build();

    var start = new Point(1, 1);
    var finish = new Point(size - 2, size - 2);
    var wave = new MazeWaveGenerator<TutorialCell>(maze).GenerateWave(start.X, start.Y, finish.X, finish.Y);
    var path = new MazePathBuilder(wave).BuildPath();
    var pathPoints = path.Cells.Select(cell => cell.Point).ToHashSet();

    RenderMaze(maze, pathPoints, start, finish);
    Console.WriteLine(path.End is null
        ? "No path could be found."
        : $"Shortest path length: {path.Length} cells");
}

static void RenderMaze(Maze<TutorialCell> maze, HashSet<Point>? path = null, Point? start = null, Point? finish = null)
{
    for (var y = 0; y < maze.Height; y++)
    {
        var buffer = new char[maze.Width];
        for (var x = 0; x < maze.Width; x++)
        {
            buffer[x] = maze.IsWall(x, y) ? '#' : ' ';
            if (path != null && path.Contains(new Point(x, y)))
            {
                buffer[x] = '*';
            }

            if (start is { } s && s.X == x && s.Y == y)
            {
                buffer[x] = 'S';
            }

            if (finish is { } f && f.X == x && f.Y == y)
            {
                buffer[x] = 'E';
            }
        }

        Console.WriteLine(new string(buffer));
    }
}

public sealed class TutorialCell : ICell
{
    private bool _isWall;

    public bool IsWall() => _isWall;

    public void MakeWall() => _isWall = true;
}

public sealed class SnapshotPlotter : IMazePlotter
{
    private readonly char[,] _canvas;

    public SnapshotPlotter(int width, int height)
    {
        _canvas = new char[height, width];
        for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
            _canvas[y, x] = ' ';
    }

    public void PlotWall(int x, int y)
    {
        _canvas[y, x] = '#';
    }

    public void PlotPassage(int x, int y)
    {
        _canvas[y, x] = ' ';
    }

    public string Render()
    {
        var builder = new StringBuilder();
        for (var y = 0; y < _canvas.GetLength(0); y++)
        {
            for (var x = 0; x < _canvas.GetLength(1); x++)
            {
                builder.Append(_canvas[y, x]);
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }
}
