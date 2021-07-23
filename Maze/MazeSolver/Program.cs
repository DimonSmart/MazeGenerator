using Maze;

namespace MazeSolver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var maze = new MazeData(new MazeSize {Height = 10, Width = 10});
            new MazeBuilder(maze).Build();
            maze.Print();
        }
    }
}