using DimonSmart.MazeGenerator;

namespace DimonSmart.MazeGeneratorConsoleDemo;

public class Cell : ICell
{
    private bool _isWall;

    public bool IsWall()
    {
        return _isWall;
    }

    public void MakeWall()
    {
        _isWall = true;
    }
}