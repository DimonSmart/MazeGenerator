namespace DimonSmart.MazeGenerator;

public interface IMaze
{
    int Width { get; }
    int Height { get; }
    ICell this[int x, int y] { get; set; }
    bool IsWall(int x, int y);
    void MakeWall(int x, int y);
}