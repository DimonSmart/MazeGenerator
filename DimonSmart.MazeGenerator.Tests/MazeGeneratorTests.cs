using Xunit;

namespace DimonSmart.MazeGenerator.Tests;

public class MazeGeneratorTests
{
    [Fact]
    public void MazeWaveGenerator_FindsShortestPath()
    {
        var maze = CreateOpenMaze(5, 5);
        var start = new Point(1, 1);
        var finish = new Point(3, 3);

        var wave = new MazeWaveGenerator<TestCell>(maze).GenerateWave(start.X, start.Y, finish.X, finish.Y);

        Assert.Equal(finish, wave.EndPoint);
        var expectedWaveNumber = Math.Abs(finish.X - start.X) + Math.Abs(finish.Y - start.Y) + 1;
        Assert.Equal(expectedWaveNumber, wave.Wave[finish.Y, finish.X]);

        var path = new MazePathBuilder(wave).BuildPath();
        Assert.Equal(start, path.Start);
        Assert.Equal(finish, path.End);
        Assert.Equal(expectedWaveNumber, path.Length);

        Assert.All(path.Cells.Zip(path.Cells.Skip(1), (current, next) => current.WaveNumber - next.WaveNumber), diff => Assert.Equal(1, diff));
    }

    [Fact]
    public void MazeWaveGenerator_ReturnsNullEndPoint_WhenTargetUnreachable()
    {
        var maze = CreateOpenMaze(5, 5);
        for (var y = 0; y < maze.Height; y++)
        {
            maze.MakeWall(2, y);
        }

        var wave = new MazeWaveGenerator<TestCell>(maze).GenerateWave(1, 1, 3, 3);
        Assert.Null(wave.EndPoint);

        var path = new MazePathBuilder(wave).BuildPath();
        Assert.Null(path.End);
        Assert.Equal(0, path.Length);
        Assert.Empty(path.Cells);
    }

    private static Maze<TestCell> CreateOpenMaze(int width, int height)
    {
        var maze = new Maze<TestCell>(width, height);
        for (var x = 0; x < width; x++)
        {
            maze.MakeWall(x, 0);
            maze.MakeWall(x, height - 1);
        }

        for (var y = 0; y < height; y++)
        {
            maze.MakeWall(0, y);
            maze.MakeWall(width - 1, y);
        }

        return maze;
    }

    private sealed class TestCell : ICell
    {
        private bool _isWall;

        public bool IsWall() => _isWall;

        public void MakeWall() => _isWall = true;
    }
}
