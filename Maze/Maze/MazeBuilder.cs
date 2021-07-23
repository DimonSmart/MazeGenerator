using System;

namespace Maze
{
    public class MazeBuilder
    {
        private readonly MazeData _mazeData;
        private readonly Random _random = new Random();

        public MazeBuilder(MazeData mazeData)
        {
            _mazeData = mazeData;
        }

        public void Build()
        {
            DrawBorder();
            Construct();
        }

        public void CreateWall(int x, int y)
        {
            _mazeData.Data[x, y] = 1;
        }

        public void DrawBorder()
        {
            for (var i = 0; i < _mazeData.MazeSize.Height; i = i + 1)
            {
                CreateWall(0, i);
                CreateWall(_mazeData.MazeSize.Width - 1, i);
            }

            for (var i = 0; i < _mazeData.MazeSize.Width; i = i + 1)
            {
                CreateWall(i, 0);
                CreateWall(i, _mazeData.MazeSize.Height - 1);
            }
        }

        public void Construct()
        {
            for (var y = 2; y < _mazeData.MazeSize.Height - 2; y = y + 2)
            for (var x = 2; x < _mazeData.MazeSize.Width - 2; x = x + 2)
                DrawLine(new Point(x, y));
        }

        public void DrawLine(Point startPoint)
        {
            var randomNumber = _random.Next(0, 4);
            switch (randomNumber)
            {
                case 0:
                    DrawLine(startPoint, 0, 1);
                    break;
                case 1:
                    DrawLine(startPoint, 1, 0);
                    break;
                case 2:
                    DrawLine(startPoint, -1, 0);
                    break;
                case 3:
                    DrawLine(startPoint, 0, -1);
                    break;
            }
        }

        public void DrawLine(Point startPoint, int dx, int dy)
        {
            var length = 0;
            var maxLength = _random.Next(1, Math.Max(_mazeData.MazeSize.Width, _mazeData.MazeSize.Height) / 4);
            while (true)
            {
                if (_mazeData.Data[startPoint.X, startPoint.Y] == 1) break;
                CreateWall(startPoint);
                if (startPoint.X % 2 == 0 && startPoint.Y % 2 == 0 && length > maxLength) break;
                startPoint.X += dx;
                startPoint.Y += dy;
                length++;
            }
        }

        public void CreateWall(Point point)
        {
            CreateWall(point.X, point.Y);
        }
    }
}