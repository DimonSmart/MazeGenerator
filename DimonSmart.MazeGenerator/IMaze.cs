namespace DimonSmart.MazeGenerator;

public interface IMaze<T> where T : ICell
{
    int Width { get; }
    int Height { get; }
    T this[int x, int y] { get; set; }
    bool IsWall(int x, int y);
    void MakeWall(int x, int y);
}
