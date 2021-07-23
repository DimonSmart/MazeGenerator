namespace Maze
{
    public class MazeData : IMaze
    {
        public readonly int[,] Data;

        public MazeSize MazeSize { get; set; }

        public MazeData(MazeSize mazeSize)
        {
            Data = new int[mazeSize.Height, mazeSize.Width];
            MazeSize = mazeSize;
        }

        public bool GetWall(Point point)
        {
            if (Data[point.X, point.Y] == 1) return true;
            return false;
        }
    }
}