namespace DimonSmart.MazeGenerator;

public static class MazeVisualization
{
    public static IMaze Redraw(this IMaze maze, IMazePlotter plotter)
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