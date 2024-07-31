namespace MazeGenerator
{
    public interface IMazePlotter
    {
        void PlotWall(int x, int y);
        void PlotPassage(int x, int y);
    }
}