namespace DimonSmart.MazeGenerator;

public class Maze<TCell> : IMaze where TCell : ICell, new()
{
    private readonly TCell[,] _field;

    public Maze(int width, int height, Func<int, int, TCell>? createCell = null)
    {
        Width = width;
        Height = height;
        _field = new TCell[Height, Width];
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                _field[y, x] = createCell != null ? createCell(x, y) : new TCell();
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

    public int Width { get; }
    public int Height { get; }

    ICell IMaze.this[int x, int y]
    {
        get => this[x, y];
        set => this[x, y] = (TCell)value;
    }

    public bool IsWall(int x, int y)
    {
        return _field[y, x].IsWall();
    }

    public void MakeWall(int x, int y)
    {
        _field[y, x].MakeWall();
    }

    public void ValidateCoordinates(int x, int y)
    {
        if (x < 0 || x >= Width)
        {
            throw new ArgumentOutOfRangeException(nameof(x), x, $"X coordinate {x} is out of the maze boundaries. Valid range is 0 to {Width - 1}.");
        }

        if (y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(nameof(y), y, $"Y coordinate {y} is out of the maze boundaries. Valid range is 0 to {Height - 1}.");
        }
    }

    public bool AreCoordinatesValid(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
