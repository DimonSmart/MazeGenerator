using static DimonSmart.MazeGenerator.MazePathFinder;

namespace DimonSmart.MazeGenerator
{
    public interface IMazePathFinder
    {
        PathFindingResult FindPath(int startX, int startY, Func<int, int, bool> criteria, CancellationToken cancellationToken = default);
        Task<PathFindingResult> FindPathAsync(int startX, int startY, Func<int, int, bool> criteria, CancellationToken cancellationToken = default);
        PathFindingResult FindPath(int startX, int startY, int endX, int endY, CancellationToken cancellationToken = default);
        Task<PathFindingResult> FindPathAsync(int startX, int startY, int endX, int endY, CancellationToken cancellationToken = default);
    }
}
