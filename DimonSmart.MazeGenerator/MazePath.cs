namespace DimonSmart.MazeGenerator;

public record MazePath(Point Start, Point? End, int Length, List<PathCell> Cells);