using DimonSmart.MazeGenerator;

namespace MazeGeneratorConsoleDemo;

public class MazeConsolePlotter : IMazePlotter, IWavePlotter, IPathPlotter
{
    // On some monitors this character printed as not visible
    // so we need a trick with foreground and background colors
    private const string Wall = "▓▓";

    // Skip 1 for skip black
    private static readonly ConsoleColor[] colors = ((ConsoleColor[])Enum.GetValues(typeof(ConsoleColor))).Skip(1)
        .ToArray();

    public TimeSpan WallDrawDelay { get; } = TimeSpan.FromMilliseconds(25);

    void IMazePlotter.PlotWall(int x, int y)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.White;
        Console.SetCursorPosition(x * 2, y);
        Console.WriteLine(Wall);
        Thread.Sleep(WallDrawDelay);
    }

    void IMazePlotter.PlotPassage(int x, int y)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(x * 2, y);
        Console.WriteLine(Wall);
    }

    public void PlotPath(int x, int y, int waveNumber, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.SetCursorPosition(x * 2, y);
        Console.WriteLine($"{waveNumber:00}");
    }

    void IWavePlotter.PlotWave(int x, int y, int waveNumber, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = colors[waveNumber % colors.Length];
        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(x * 2, y);
        Console.WriteLine($"{waveNumber:00}");
        Thread.Sleep(WallDrawDelay);
    }
}