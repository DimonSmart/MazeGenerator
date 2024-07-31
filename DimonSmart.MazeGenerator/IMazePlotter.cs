namespace DimonSmart.MazeGenerator
{
    /// <summary>
    /// Provides functionality for plotting walls and passages in a maze.
    /// </summary>
    public interface IMazePlotter
    {
        /// <summary>
        /// Plots a wall at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the wall.</param>
        /// <param name="y">The y-coordinate of the wall.</param>
        void PlotWall(int x, int y);

        /// <summary>
        /// Plots a passage at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the passage.</param>
        /// <param name="y">The y-coordinate of the passage.</param>
        void PlotPassage(int x, int y);
    }
}