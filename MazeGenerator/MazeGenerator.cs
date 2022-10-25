namespace MazeGenerator
{
    public class MazeBuilder<ICell> 
    {
        private readonly Maze _maze;

        public MazeGenerateOptions _options { get; }

        public MazeBuilder(Maze maze, MazeGenerateOptions options)
        {
            _maze = maze;
            _options = options;
        }

        public void Generate()
        {
            for (var y = 2; y < _maze.Height - 2; y += 2)
                for (var x = 2; x < _maze.Width - 2; x += 2)
                    DrawLine(x, y);
        }

        public void DrawLine(int x, int y)
        {
            var randomNumber = Random.Shared.Next(0, 4);
            switch (randomNumber)
            {
                case 0:
                    DrawLine(x, y, 0, 1);
                    break;
                case 1:
                    DrawLine(x, y, 1, 0);
                    break;
                case 2:
                    DrawLine(x, y, -1, 0);
                    break;
                case 3:
                    DrawLine(x, y, 0, -1);
                    break;
            }
        }

        public void DrawLine(int x, int y, int dx, int dy)
        {
            var length = 0;
            while (true)
            {
                if (_maze.IsWall(x,y)) break;
                _maze.MakeWall(x, y);
                MazeVizualization.Redraw(_maze);
                if (length > 1 && x % 2 == 0 && y % 2 == 0 && Random.Shared.NextDouble() < _options.StopWallGenerationProbability) break;
                x += dx;
                y += dy;
                length++;
            }
        }
    }
}