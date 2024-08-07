namespace DimonSmart.MazeGenerator
{
    public class MazePathFinder : IMazePathFinder
    {
        private readonly IMaze _maze;
        private readonly IWavePlotter? _wavePlotter;
        private int[,]? _wave;

        public record Point(int X, int Y);
        public record PathFindingResult(Point? EndPoint, int[,] Wave);

        public MazePathFinder(IMaze maze, IWavePlotter? wavePlotter = null)
        {
            _maze = maze;
            _wavePlotter = wavePlotter;
        }

        public PathFindingResult FindPath(int startX, int startY, Func<int, int, bool> criteria)
        {
            return FindPathCoreAsync(startX, startY, criteria, (x, y, waveNumber) =>
            {
                SetWavePoint(x, y, waveNumber);
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();
        }

        public Task<PathFindingResult> FindPathAsync(int startX, int startY, Func<int, int, bool> criteria)
        {
            return FindPathCoreAsync(startX, startY, criteria, SetWavePointAsync);
        }

        public PathFindingResult FindPath(int startX, int startY, int endX, int endY)
        {
            return FindPath(startX, startY, (x, y) => x == endX && y == endY);
        }

        public Task<PathFindingResult> FindPathAsync(int startX, int startY, int endX, int endY)
        {
            return FindPathAsync(startX, startY, (x, y) => x == endX && y == endY);
        }

        private async Task<PathFindingResult> FindPathCoreAsync(int startX, int startY, Func<int, int, bool> criteria, Func<int, int, int, Task> setWavePoint)
        {
            var stack1 = new Stack<Point>();
            var stack2 = new Stack<Point>();
            _wave = new int[_maze.Height, _maze.Width];
            stack1.Push(new Point(startX, startY));
            _wave[startY, startX] = 1;
            do
            {
                while (stack1.Any())
                {
                    var (x, y) = stack1.Pop();
                    var waveNumber = _wave[y, x];
                    await setWavePoint(x, y, waveNumber);

                    if (criteria(x, y)) { return new PathFindingResult(new Point(x, y), _wave); }
                    waveNumber++;

                    TryPush(stack2, x + 1, y, waveNumber);
                    TryPush(stack2, x - 1, y, waveNumber);
                    TryPush(stack2, x, y + 1, waveNumber);
                    TryPush(stack2, x, y - 1, waveNumber);
                }

                (stack1, stack2) = (stack2, stack1);
            } while (stack1.Any());
            return new PathFindingResult(null, _wave); ;
        }

        private void TryPush(Stack<Point> stack, int x, int y, int waveNumber)
        {
            if (!_maze.IsWall(x, y) && _wave![y, x] == 0)
            {
                stack.Push(new Point(x, y));
                SetWavePoint(x, y, waveNumber);
            }
        }

        private void SetWavePoint(int x, int y, int waveNumber)
        {
            _wave![y, x] = waveNumber;
            _wavePlotter?.PlotWave(x, y, waveNumber);
        }

        private async Task SetWavePointAsync(int x, int y, int waveNumber)
        {
            _wave![y, x] = waveNumber;
            if (_wavePlotter != null)
            {
                await _wavePlotter.PlotWaveAsync(x, y, waveNumber);
            }
        }
    }
}