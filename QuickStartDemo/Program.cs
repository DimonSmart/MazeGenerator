using DimonSmart.MazeGenerator;

const int width = 31;
const int height = 21;

var maze = new Maze<TutorialCell>(width, height);
var options = new MazeBuildOptions(WallShortness: 0.15, Emptiness: 0.05);
var builder = new MazeBuilder<TutorialCell>(maze, options);
builder.Build();

var start = new Point(1, 1);
var finish = new Point(width - 2, height - 2);
var wave = new MazeWaveGenerator<TutorialCell>(maze).GenerateWave(start.X, start.Y, finish.X, finish.Y);
var path = new MazePathBuilder(wave).BuildPath();

RenderMaze(maze, path, start, finish);

static void RenderMaze(Maze<TutorialCell> maze, MazePath path, Point start, Point finish)
{
    var pathPoints = path.Cells.Select(cell => cell.Point).ToHashSet();

    for (var y = 0; y < maze.Height; y++)
    {
        var buffer = new char[maze.Width];
        for (var x = 0; x < maze.Width; x++)
        {
            buffer[x] = maze.IsWall(x, y) ? '#' : ' ';
        }

        foreach (var point in pathPoints.Where(p => p.Y == y))
        {
            buffer[point.X] = '*';
        }

        if (start.Y == y)
        {
            buffer[start.X] = 'S';
        }

        if (finish.Y == y)
        {
            buffer[finish.X] = 'E';
        }

        Console.WriteLine(new string(buffer));
    }

    Console.WriteLine();
    Console.WriteLine(path.End is null
        ? "No path could be found."
        : $"Path length: {path.Length} cells");
}

public sealed class TutorialCell : ICell
{
    private bool _isWall;

    public bool IsWall() => _isWall;

    public void MakeWall() => _isWall = true;
}
