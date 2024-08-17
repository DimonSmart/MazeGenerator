namespace DimonSmart.MazeGenerator;

/// <summary>
/// Represents a single cell within a maze.
/// </summary>
public interface ICell
{
    /// <summary>
    /// Determines if the cell is a wall.
    /// </summary>
    /// <returns>A boolean indicating if the cell is a wall.</returns>
    bool IsWall();

    /// <summary>
    /// Sets the cell to be a wall.
    /// </summary>
    void MakeWall();
}