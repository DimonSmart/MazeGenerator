namespace DimonSmart.MazeGenerator
{

    public class MazeWaveGenerator(IMaze maze, IWavePlotter? wavePlotter = null) : IMazeWaveGenerator
    {
        private int[,]? _wave;

        public MazeWave GenerateWave(int startX, int startY, Func<int, int, bool> criteria, CancellationToken cancellationToken)
        {
            return GenerateWaveCoreAsync(startX, startY, criteria, (x, y, waveNumber, ct) =>
            {
                SetWavePoint(x, y, waveNumber, cancellationToken);
                return Task.CompletedTask;
            }, cancellationToken).GetAwaiter().GetResult();
        }

        public Task<MazeWave> GenerateWaveAsync(int startX, int startY, Func<int, int, bool> criteria, CancellationToken cancellationToken = default)
        {
            return GenerateWaveCoreAsync(startX, startY, criteria, SetWavePointAsync, cancellationToken);
        }

        public MazeWave GenerateWave(int startX, int startY, int endX, int endY, CancellationToken cancellationToken = default)
        {
            return GenerateWave(startX, startY, (x, y) => x == endX && y == endY, cancellationToken);
        }

        public Task<MazeWave> GenerateWaveAsync(int startX, int startY, int endX, int endY, CancellationToken cancellationToken = default)
        {
            return GenerateWaveAsync(startX, startY, (x, y) => x == endX && y == endY, cancellationToken);
        }

        private async Task<MazeWave> GenerateWaveCoreAsync(int startX, int startY, Func<int, int, bool> criteria, Func<int, int, int, CancellationToken, Task> setWavePoint, CancellationToken cancellationToken)
        {
            var stack1 = new Stack<Point>();
            var stack2 = new Stack<Point>();
            var startPoint = new Point(startX, startY);
            _wave = new int[maze.Height, maze.Width];
            stack1.Push(startPoint);
            _wave[startY, startX] = 1;
            do
            {
                while (stack1.Any())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var (x, y) = stack1.Pop();
                    var waveNumber = _wave[y, x];
                    await setWavePoint(x, y, waveNumber, cancellationToken);

                    if (criteria(x, y)) { return new MazeWave(startPoint, new Point(x, y), _wave); }
                    waveNumber++;

                    TryPush(stack2, x + 1, y, waveNumber, cancellationToken);
                    TryPush(stack2, x - 1, y, waveNumber, cancellationToken);
                    TryPush(stack2, x, y + 1, waveNumber, cancellationToken);
                    TryPush(stack2, x, y - 1, waveNumber, cancellationToken);
                }

                (stack1, stack2) = (stack2, stack1);
            } while (stack1.Any());
            return new MazeWave(startPoint, null, _wave);
        }

        private void TryPush(Stack<Point> stack, int x, int y, int waveNumber, CancellationToken cancellationToken)
        {
            if (!maze.IsWall(x, y) && _wave![y, x] == 0)
            {
                stack.Push(new Point(x, y));
                SetWavePoint(x, y, waveNumber, cancellationToken);
            }
        }

        private void SetWavePoint(int x, int y, int waveNumber, CancellationToken cancellationToken)
        {
            _wave![y, x] = waveNumber;
            wavePlotter?.PlotWave(x, y, waveNumber, cancellationToken);
        }

        private async Task SetWavePointAsync(int x, int y, int waveNumber, CancellationToken cancellationToken)
        {
            _wave![y, x] = waveNumber;
            if (wavePlotter != null)
            {
                await wavePlotter.PlotWaveAsync(x, y, waveNumber, cancellationToken);
            }
        }
    }
}