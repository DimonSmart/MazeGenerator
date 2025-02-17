namespace DimonSmart.MazeGenerator;

public class MazeBuilder<TCell>(IMaze<TCell> Maze, MazeBuildOptions Options) : IMazeBuilder where TCell : ICell
{
    private bool _done;

    public void Build(IMazePlotter? plotter = null, CancellationToken cancellationToken = default)
    {
        BuildCoreAsync((x, y) =>
        {
            plotter?.PlotWall(x, y);
            return Task.CompletedTask;
        }, cancellationToken).GetAwaiter().GetResult();
    }

    public async Task BuildAsync(IMazePlotter? plotter = null, CancellationToken cancellationToken = default)
    {
        Func<int, int, Task> plotAction = plotter != null
            ? plotter.PlotWallAsync
            : (x, y) => Task.CompletedTask;

        await BuildCoreAsync(plotAction, cancellationToken);
    }

    private async Task DrawLine(int x, int y, Func<int, int, Task> plotAction, CancellationToken cancellationToken)
    {
        var randomNumber = Random.Shared.Next(0, 4);
        switch (randomNumber)
        {
            case 0:
                await DrawLine(x, y, 0, 1, plotAction, cancellationToken);
                break;
            case 1:
                await DrawLine(x, y, 1, 0, plotAction, cancellationToken);
                break;
            case 2:
                await DrawLine(x, y, -1, 0, plotAction, cancellationToken);
                break;
            case 3:
                await DrawLine(x, y, 0, -1, plotAction, cancellationToken);
                break;
        }
    }

    private async Task DrawLine(int x, int y, int dx, int dy, Func<int, int, Task> plotAction,
        CancellationToken cancellationToken)
    {
        if (Random.Shared.NextDouble() < Options.Emptiness)
        {
            return;
        }

        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (Maze.IsWall(x, y))
            {
                break;
            }

            Maze.MakeWall(x, y);
            await plotAction(x, y);
            if (x % 2 == 0 && y % 2 == 0 && Random.Shared.NextDouble() < Options.WallShortness)
            {
                break;
            }

            x += dx;
            y += dy;
        }
    }

    private async Task BuildCoreAsync(Func<int, int, Task> plotAction, CancellationToken cancellationToken)
    {
        if (_done)
        {
            return;
        }

        await CreateBorder(plotAction, cancellationToken);
        foreach (var point in GetAllPossibleStartPoints().OrderBy(_ => Random.Shared.Next()))
        {
            await DrawLine(point.X, point.Y, plotAction, cancellationToken);
        }

        _done = true;
    }

    public IEnumerable<Point> GetAllPossibleStartPoints()
    {
        for (var y = 2; y < Maze.Height - 2; y += 2)
            for (var x = 2; x < Maze.Width - 2; x += 2)
                yield return new Point(x, y);
    }

    private async Task CreateBorder(Func<int, int, Task> plotAction, CancellationToken cancellationToken)
    {
        for (var y = 0; y < Maze.Height; y++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Maze.MakeWall(0, y);
            if (plotAction != null)
            {
                await plotAction(0, y);
            }

            Maze.MakeWall(Maze.Width - 1, y);
            if (plotAction != null)
            {
                await plotAction(Maze.Width - 1, y);
            }
        }

        for (var x = 0; x < Maze.Width; x++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Maze.MakeWall(x, 0);
            if (plotAction != null)
            {
                await plotAction(x, 0);
            }

            Maze.MakeWall(x, Maze.Height - 1);
            if (plotAction != null)
            {
                await plotAction(x, Maze.Height - 1);
            }
        }
    }
}
