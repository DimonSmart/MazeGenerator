namespace DimonSmart.MazeGenerator;

public static class MazeVisualization
{
    public static IMaze<T> Redraw<T>(this IMaze<T> maze, IMazePlotter plotter) where T : ICell
    {
        for (var y = 0; y < maze.Height; y++)
            for (var x = 0; x < maze.Width; x++)
                if (maze.IsWall(x, y))
                {
                    plotter.PlotWall(x, y);
                }
                else
                {
                    plotter.PlotPassage(x, y);
                }

        return maze;
    }
}