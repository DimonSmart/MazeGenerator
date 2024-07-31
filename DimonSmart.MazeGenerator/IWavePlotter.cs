namespace MazeGenerator
{
    public interface IMazePlotter
    {
        void PlotWave(int x, int y, int waveNumber);
        void PlotWall(int x, int y);
        void PlotPassage(int x, int y);
    }
}