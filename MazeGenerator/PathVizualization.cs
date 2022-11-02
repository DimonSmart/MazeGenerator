using static MazeGenerator.MazePathFinder;

namespace MazeGenerator
{
    public static class PathVizualization
    {
        public static void VizualizePath(this PathFindingResult path)
        {
            Console.CursorVisible = false;

            var x = path.EndPoint.X;
            var y = path.EndPoint.Y;
            var currentWaveNumber = path.Wave[y, x];
            while (currentWaveNumber > 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(x * 2, y);
                Console.WriteLine($"{currentWaveNumber:00}");

                currentWaveNumber--;

                if (path.Wave[y, x + 1] == currentWaveNumber)
                {
                    x++; continue;
                }
                if (path.Wave[y, x - 1] == currentWaveNumber)
                {
                    x--; continue;
                }
                if (path.Wave[y + 1, x] == currentWaveNumber)
                {
                    y++; continue;
                }
                if (path.Wave[y - 1, x] == currentWaveNumber)
                {
                    y--; continue;
                }
                break;
            }
        }
    }
}