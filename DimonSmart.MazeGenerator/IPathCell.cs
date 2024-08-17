namespace DimonSmart.MazeGenerator;

public interface IPathCell
{
    Point Point { get; }
    int WaveNumber { get; }
}