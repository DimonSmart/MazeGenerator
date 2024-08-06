namespace DimonSmart.MazeGenerator
{
    public static class MazePathFinder
    {
        public record Point(int X, int Y);
        public record PathFindingResult(Point EndPoint, int[,] Wave);
        public static PathFindingResult? FindPath(this IMaze maze, int startX, int startY, Func<int, int, bool> criteria, Action<int[,]> onWave)
        {

            var stack1 = new Stack<Point>();
            var stack2 = new Stack<Point>();
            var wave = new int[maze.Height, maze.Width];
            stack1.Push(new Point(startX, startY));
            wave[startY, startX] = 1;
            do
            {
                while (stack1.Any())
                {

                    var (x, y) = stack1.Pop();
                    var waveNumber = wave[y, x];
                    wave[y, x] = waveNumber;

                    if (criteria(x, y)) { return new PathFindingResult(new Point(x, y), wave); }
                    waveNumber++;

                    if (!maze.IsWall(x + 1, y) && wave[y, x + 1] == 0)
                    {
                        stack2.Push(new Point(x + 1, y)); wave[y, x + 1] = waveNumber;
                    }
                    if (!maze.IsWall(x - 1, y) && wave[y, x - 1] == 0)
                    {
                        stack2.Push(new Point(x - 1, y)); wave[y, x - 1] = waveNumber;
                    }
                    if (!maze.IsWall(x, y + 1) && wave[y + 1, x] == 0)
                    {
                        stack2.Push(new Point(x, y + 1)); wave[y + 1, x] = waveNumber;
                    }
                    if (!maze.IsWall(x, y - 1) && wave[y - 1, x] == 0)
                    {
                        stack2.Push(new Point(x, y - 1)); wave[y - 1, x] = waveNumber;
                    }
                }
                onWave(wave);
                (stack1, stack2) = (stack2, stack1);
            } while (stack1.Any());
            return null;
        }
    }
}