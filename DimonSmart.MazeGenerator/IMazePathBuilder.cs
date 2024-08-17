namespace DimonSmart.MazeGenerator;

public interface IMazePathBuilder
{
    MazePath BuildPath(Point endPoint);
}