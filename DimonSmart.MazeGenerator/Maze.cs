namespace DimonSmart.MazeGenerator
{
    public class Maze<TCell> : IMaze where TCell : ICell, new()
    {
        public int Width { get; }
        public int Height { get; }
        private readonly TCell[,] _field;

        public Maze(int width, int height, Func<int, int, TCell>? createCell = null)
        {
            Width = width;
            Height = height;
            _field = new TCell[Height, Width];
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    _field[y, x] = createCell != null ? createCell(x, y) : new TCell();
                }
            }
        }

        public TCell this[int x, int y]
        {
            get
            {
                ValidateCoordinates(x, y);
                return _field[y, x];
            }
            set
            {
                ValidateCoordinates(x, y);
                _field[y, x] = value;
            }
        }

        ICell IMaze.this[int x, int y]
        {
            get => this[x, y];
            set => this[x, y] = (TCell)value;
        }

        public bool IsWall(int x, int y) => _field[y, x].IsWall();
        public void MakeWall(int x, int y) => _field[y, x].MakeWall();

        private void ValidateCoordinates(int x, int y)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentOutOfRangeException(nameof(x), "Coordinates out of the maze boundaries.");
            if (y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException(nameof(y), "Coordinates out of the maze boundaries.");
        }
    }
}