namespace DimonSmart.MazeGenerator
{
    public record struct PathCell(Point Point, int WaveNumber) : IPathCell;
}
