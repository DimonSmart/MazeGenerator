using DimonSmart.MazeGenerator;

namespace MazeGenerator
{
    public class Maze
    {
        public readonly int Width;
        public readonly int Height;
        private readonly IMazePlotter? _mazePlotter;
        private readonly ICell[,] field;

        public Maze(int width, int height, Func<ICell> createCell, IMazePlotter? mazePlotter = default)
        {
            Width = width;
            Height = height;
            _mazePlotter = mazePlotter;
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

        public bool IsWall(int x, int y) => field[y, x].IsWall();
        public void MakeWall(int x, int y)
        {
            field[y, x].MakeWall();
            _mazePlotter?.PlotWall(x, y);
        }
    }

}