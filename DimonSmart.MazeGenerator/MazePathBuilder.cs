namespace DimonSmart.MazeGenerator;

public class MazePathBuilder(MazeWave mazeWave, IPathPlotter? pathPlotter = null)
{
    public MazePath BuildPath()
    {
        return BuildPathCore((x, y, waveNumber) =>
        {
            pathPlotter?.PlotPath(x, y, waveNumber);
            return Task.CompletedTask;
        }).Result;
    }

    public async Task<MazePath> BuildPathAsync()
    {
        return await BuildPathCore(async (x, y, waveNumber) =>
        {
            if (pathPlotter != null)
            {
                await pathPlotter.PlotPathAsync(x, y, waveNumber);
            }
        });
    }

    private async Task<MazePath> BuildPathCore(Func<int, int, int, Task> plotPath)
    {
        var endPoint = mazeWave.EndPoint;
        if (endPoint == null)
        {
            return new MazePath(mazeWave.StartPoint, null, 0, new List<PathCell>());
        }

        var length = mazeWave.Wave[endPoint.Y, endPoint.X];
        var pathCells = new List<PathCell>(length);
        var currentPoint = endPoint;
        var wave = mazeWave.Wave;

        while (true)
        {
            pathCells.Add(new PathCell(currentPoint, wave[currentPoint.Y, currentPoint.X]));
            await plotPath(currentPoint.X, currentPoint.Y, wave[currentPoint.Y, currentPoint.X]);

            var previousPoint = GetPreviousPoint(currentPoint, wave);
            if (previousPoint == null)
            {
                break;
            }

            currentPoint = previousPoint;
        }

        return new MazePath(currentPoint, endPoint, pathCells.Count, pathCells);
    }

    private static Point? GetPreviousPoint(Point point, int[,] wave)
    {
        var x = point.X;
        var y = point.Y;
        var waveNumber = wave[y, x];
        if (waveNumber == 1)
        {
            return null;
        }

        if (x > 0 && wave[y, x - 1] == waveNumber - 1)
        {
            return new Point(x - 1, y);
        }

        if (x < wave.GetLength(1) - 1 && wave[y, x + 1] == waveNumber - 1)
        {
            return new Point(x + 1, y);
        }

        if (y > 0 && wave[y - 1, x] == waveNumber - 1)
        {
            return new Point(x, y - 1);
        }

        if (y < wave.GetLength(0) - 1 && wave[y + 1, x] == waveNumber - 1)
        {
            return new Point(x, y + 1);
        }

        return null;
    }
}