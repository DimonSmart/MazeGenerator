namespace DimonSmart.MazeGenerator
{
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
        void PlotWave(int x, int y, int waveNumber);
    }
}