using MazeGenerator;

namespace MazeGeneratorConsoleDemo
{
    public class WaveConsolePlotter : IWavePlotter
    {
        // Skip 1 for skip black
        private static readonly ConsoleColor[] colors = ((ConsoleColor[])Enum.GetValues(typeof(ConsoleColor))).Skip(1).ToArray();

        public void PlotWave(int x, int y, int waveNumber)
        {
            Console.ForegroundColor = colors[waveNumber % colors.Length];
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(waveNumber);
        }
    }
}