namespace MazeGenerator
{
    public static class WaveVisualization
    {
        public static void VisualizeWave(this int[,] wave, IMazePlotter plotter)
        {
            for (var y = 0; y < wave.GetLength(0); y++)
            {
                for (var x = 0; x < wave.GetLength(1); x++)
                {
                    var waveNumber = wave[y, x];
                    if (waveNumber != 0)
                    {
                        plotter.PlotWave(x, y, wave[y, x]);
                    }
                }
            }
        }
    }
}