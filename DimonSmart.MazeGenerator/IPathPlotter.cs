namespace MazeGenerator
{
    /// <summary>
    /// Provides functionality for plotting paths in a maze.
    /// </summary>
    public interface IPathPlotter
    {
        /// <summary>
        /// Plots a path at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the path.</param>
        /// <param name="y">The y-coordinate of the path.</param>
        /// <param name="waveNumber">The wave number indicating the order of path plotting.</param>
        void PlotPath(int x, int y, int waveNumber);
    }
}