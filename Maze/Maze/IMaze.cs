namespace Maze
{
    public interface IMaze
    {
        bool GetWall(Point point);

        MazeSize MazeSize { get; }
    }
}