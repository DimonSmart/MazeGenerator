﻿namespace DimonSmart.MazeGenerator
{
    public class MazeBuilder<ICell>
    {
        private readonly Maze _maze;
        private readonly MazeBuildOptions Options;
        private bool _done;

        public MazeBuilder(Maze maze, MazeBuildOptions options)
        {
            _maze = maze;
            Options = options;
        }

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

        private async Task DrawLine(int x, int y, int dx, int dy, Func<int, int, Task> plotAction, CancellationToken cancellationToken)
        {
            if (Random.Shared.NextDouble() < Options.Emptiness) return;
            var length = 0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested) return;
                if (_maze.IsWall(x, y)) break;
                _maze.MakeWall(x, y);
                await plotAction(x, y);
                if (length > 1 && x % 2 == 0 && y % 2 == 0 && Random.Shared.NextDouble() < Options.StopWallGenerationProbability) break;
                x += dx;
                y += dy;
                length++;
            }
        }

        private async Task BuildCoreAsync(Func<int, int, Task> plotAction, CancellationToken cancellationToken)
        {
            if (_done) return;
            await CreateBorder(plotAction, cancellationToken);
            for (var y = 2; y < _maze.Height - 2; y += 2)
            {
                for (var x = 2; x < _maze.Width - 2; x += 2)
                {
                    await DrawLine(x, y, plotAction, cancellationToken);
                }
            }

            _done = true;
        }

        private async Task CreateBorder(Func<int, int, Task> plotAction, CancellationToken cancellationToken)
        {
            for (var y = 0; y < _maze.Height; y++)
            {
                if (cancellationToken.IsCancellationRequested) return;
                _maze.MakeWall(0, y);
                if (plotAction != null) await plotAction(0, y);
                _maze.MakeWall(_maze.Width - 1, y);
                if (plotAction != null) await plotAction(_maze.Width - 1, y);
            }

            for (var x = 0; x < _maze.Width; x++)
            {
                if (cancellationToken.IsCancellationRequested) return;
                _maze.MakeWall(x, 0);
                if (plotAction != null) await plotAction(x, 0);
                _maze.MakeWall(x, _maze.Height - 1);
                if (plotAction != null) await plotAction(x, _maze.Height - 1);
            }
        }
    }
}