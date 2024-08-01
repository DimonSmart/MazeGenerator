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
        void PlotWall(int x, int y) { }

        /// <summary>
        /// Plots a passage at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the passage.</param>
        /// <param name="y">The y-coordinate of the passage.</param>
        void PlotPassage(int x, int y) { }

        /// <summary>
        /// Plots a wall asynchronously at the specified coordinates.
        /// Note: If you only use async methods, you only need to implement the async methods.
        /// </summary>
        /// <param name="x">The x-coordinate of the wall.</param>
        /// <param name="y">The y-coordinate of the wall.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task PlotWallAsync(int x, int y) => Task.CompletedTask;

        /// <summary>
        /// Plots a passage asynchronously at the specified coordinates.
        /// Note: If you only use async methods, you only need to implement the async methods.
        /// </summary>
        /// <param name="x">The x-coordinate of the passage.</param>
        /// <param name="y">The y-coordinate of the passage.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task PlotPassageAsync(int x, int y) => Task.CompletedTask;
    }
}
