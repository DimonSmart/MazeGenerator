using static MazeGenerator.MazePathFinder;

namespace MazeGenerator
{
    public static class PathVizualization
    {
        public static void VizualizePath(this PathFindingResult path, IPathPlotter plotter)
        {
            var x = path.EndPoint.X;
            var y = path.EndPoint.Y;
            var currentWaveNumber = path.Wave[y, x];
            while (currentWaveNumber > 0)
            {
                plotter.PlotPath(x, y, currentWaveNumber);
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