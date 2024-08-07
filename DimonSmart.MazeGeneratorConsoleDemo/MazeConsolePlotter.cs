using DimonSmart.MazeGenerator;

namespace MazeGeneratorConsoleDemo
{
    public class MazeConsolePlotter : IMazePlotter, IWavePlotter, IPathPlotter
    {
        // Skip 1 for skip black
        private static readonly ConsoleColor[] colors = ((ConsoleColor[])Enum.GetValues(typeof(ConsoleColor))).Skip(1).ToArray();

        // On some monitors this character printed as not visible
        // so we need a trick with foreground and background colors
        private const string Wall = "▓▓";

        public TimeSpan _wallDrawDelay { get; } = TimeSpan.FromMilliseconds(25);

        void IMazePlotter.PlotWall(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(Wall);
            Thread.Sleep(_wallDrawDelay);
        }

        void IMazePlotter.PlotPassage(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(Wall);
        }

        void IWavePlotter.PlotWave(int x, int y, int waveNumber, CancellationToken cancellationToken)
        {
            Console.ForegroundColor = colors[waveNumber % colors.Length];
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine($"{waveNumber:00}");
            Thread.Sleep(_wallDrawDelay);
        }

        public void PlotPath(int x, int y, int waveNumber)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine($"{waveNumber:00}");
        }
    }
}