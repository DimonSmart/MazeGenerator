namespace MazeGenerator
{
    public static class WaveVizualization
    {
        // Skip 1 for skip black
        private static readonly ConsoleColor[] colors = ((ConsoleColor[])Enum.GetValues(typeof(ConsoleColor))).Skip(1).ToArray();

        public static void Redraw(this int[,] wave)
        {
            Console.CursorVisible = false;
            for (int y = 0; y < wave.GetLength(0); y++)
            {
                for (int x = 0; x < wave.GetLength(1); x++)
                {
                    var waveNumber = wave[y, x];
                    if (waveNumber != 0)
                    {
                        Console.ForegroundColor = colors[waveNumber % colors.Length];
                        Console.SetCursorPosition(x * 2, y);
                        Console.WriteLine(wave[y, x]);
                    }
                }
            }
        }
    }
}