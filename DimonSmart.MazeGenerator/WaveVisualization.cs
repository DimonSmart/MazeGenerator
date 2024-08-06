namespace DimonSmart.MazeGenerator
{
    public static class WaveVisualization
    {
        public static void VisualizeWave(this int[,] wave, IWavePlotter plotter)
        {
            VisualizeWaveCore(wave, (x, y, waveNumber) =>
            {
                plotter.PlotWave(x, y, waveNumber);
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();
        }

        public static async Task VisualizeWaveAsync(this int[,] wave, IWavePlotter plotter, CancellationToken cancellationToken = default)
        {
            await VisualizeWaveCore(wave, plotter.PlotWaveAsync, cancellationToken);
        }

        private static async Task VisualizeWaveCore(int[,] wave, Func<int, int, int, Task> plotAction, CancellationToken cancellationToken = default)
        {
            for (int y = 0; y < wave.GetLength(0); y++)
            {
                for (int x = 0; x < wave.GetLength(1); x++)
                {
                    int waveNumber = wave[y, x];
                    if (waveNumber != 0)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return;

                        await plotAction(x, y, waveNumber);
                    }
                }
            }
        }
    }
}
