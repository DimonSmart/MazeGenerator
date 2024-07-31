namespace DimonSmart.MazeGenerator
{
    public class Cell : ICell
    {
        private bool _isWall;
        public bool IsWall() => _isWall;
        public void MakeWall() => _isWall = true;
    }
}