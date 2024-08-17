namespace DimonSmart.MazeGenerator;

/// <summary>
/// Provides functionality for visualizing wave propagation in a maze.
/// </summary>
public interface IWavePlotter
{
    /// <summary>
    /// Visualizes a wave at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the wave.</param>
    /// <param name="y">The y-coordinate of the wave.</param>
    /// <param name="waveNumber">The wave number to be visualized.</param>
    public void PlotWave(int x, int y, int waveNumber, CancellationToken cancellationToken = default)
    {
    }

    /// <summary>
    /// Asynchronously visualizes a wave at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the wave.</param>
    /// <param name="y">The y-coordinate of the wave.</param>
    /// <param name="waveNumber">The wave number to be visualized.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PlotWaveAsync(int x, int y, int waveNumber, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}