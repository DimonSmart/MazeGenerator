namespace DimonSmart.MazeGenerator;

public record MazeWave(Point StartPoint, Point? EndPoint, int[,] Wave);