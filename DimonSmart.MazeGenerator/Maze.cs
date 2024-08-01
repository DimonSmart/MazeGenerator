namespace DimonSmart.MazeGenerator
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
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    field[y, x] = createCell();
                }
            }

            CreateBorder();
        }

        private void CreateBorder()
        {
            for (var y = 0; y < Height; y++)
            {
                MakeWall(0, y);
                MakeWall(Width - 1, y);
            }

            for (var x = 0; x < Width; x++)
            {
                MakeWall(x, 0);
                MakeWall(x, Height - 1);
            }
        }

        public ICell this[int x, int y]
        {
            get
            {
                ValidateCoordinates(x, y);
                return field[y, x];
            }
            set
            {
                ValidateCoordinates(x, y);
                field[y, x] = value;
            }
        }

        public bool IsWall(int x, int y) => field[y, x].IsWall();
        public void MakeWall(int x, int y) => field[y, x].MakeWall();

        private void ValidateCoordinates(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException("Coordinates out of the maze boundaries.");
        }
    }

}