namespace DimonSmart.MazeGenerator
{
    public interface IMazeWaveGenerator
    {
        MazeWave GenerateWave(int startX, int startY, Func<int, int, bool> criteria, CancellationToken cancellationToken = default);
        Task<MazeWave> GenerateWaveAsync(int startX, int startY, Func<int, int, bool> criteria, CancellationToken cancellationToken = default);
        MazeWave GenerateWave(int startX, int startY, int endX, int endY, CancellationToken cancellationToken = default);
        Task<MazeWave> GenerateWaveAsync(int startX, int startY, int endX, int endY, CancellationToken cancellationToken = default);
    }
}
