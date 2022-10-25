namespace MazeGenerator
{
    public interface ICell
    {
        bool IsWall();
        void MakeWall();
    }
}