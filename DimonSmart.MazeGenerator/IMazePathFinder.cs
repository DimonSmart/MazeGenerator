using static DimonSmart.MazeGenerator.MazePathFinder;

namespace DimonSmart.MazeGenerator
{
    public interface IMazePathFinder
    {
        PathFindingResult FindPath(int startX, int startY, Func<int, int, bool> criteria);
        Task<PathFindingResult> FindPathAsync(int startX, int startY, Func<int, int, bool> criteria);
        PathFindingResult FindPath(int startX, int startY, int endX, int endY);
        Task<PathFindingResult> FindPathAsync(int startX, int startY, int endX, int endY);
    }
}
