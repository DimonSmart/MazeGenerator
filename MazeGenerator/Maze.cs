namespace MazeGenerator
{
    public class Maze
    {
        public readonly int Width;
        public readonly int Height;
        private readonly ICell[,] field;

        public Maze(int width, int height, Func<ICell> createCell)
        {
            Width = width;
            Height = height;
            field = new ICell[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    field[y, x] = createCell();
                }
            }
            for (int y = 0; y < Height; y++)
            {
                field[y, 0].MakeWall();
                field[y, Width - 1].MakeWall();
            }

            for (int x = 0; x < Width; x++)
            {
                field[0, x].MakeWall();
                field[Height-1, x].MakeWall();
            }
        }

        public bool IsWall(int x, int y) => field[y, x].IsWall();
        public void MakeWall(int x, int y) => field[y, x].MakeWall();
    }
}