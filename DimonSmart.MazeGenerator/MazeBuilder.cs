namespace DimonSmart.MazeGenerator
{
    public class MazeBuilder<ICell>
    {
        private readonly Maze _maze;
        private readonly MazeGenerateOptions Options;
        private bool _done;

        public MazeBuilder(Maze maze, MazeGenerateOptions options)
        {
            _maze = maze;
            Options = options;
        }

        public void Build() => Build(CancellationToken.None);

        public void Build(CancellationToken cancellationToken)
        {
            if (_done) return;
            for (var y = 2; y < _maze.Height - 2; y += 2)
                for (var x = 2; x < _maze.Width - 2; x += 2)
                    DrawLine(x, y, cancellationToken);
            _done = true;
        }

        private void DrawLine(int x, int y, CancellationToken cancellationToken)
        {
            var randomNumber = Random.Shared.Next(0, 4);
            switch (randomNumber)
            {
                case 0:
                    DrawLine(x, y, 0, 1, cancellationToken);
                    break;
                case 1:
                    DrawLine(x, y, 1, 0, cancellationToken);
                    break;
                case 2:
                    DrawLine(x, y, -1, 0, cancellationToken);
                    break;
                case 3:
                    DrawLine(x, y, 0, -1, cancellationToken);
                    break;
            }
        }

        private void DrawLine(int x, int y, int dx, int dy, CancellationToken cancellationToken)
        {
            if (Random.Shared.NextDouble() < Options.Emptiness) return;
            var length = 0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested) return;
                if (_maze.IsWall(x, y)) break;
                _maze.MakeWall(x, y);
                if (length > 1 && x % 2 == 0 && y % 2 == 0 && Random.Shared.NextDouble() < Options.StopWallGenerationProbability) break;
                x += dx;
                y += dy;
                length++;
            }
        }
    }
}