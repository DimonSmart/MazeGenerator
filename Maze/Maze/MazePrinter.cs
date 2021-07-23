using System;
using System.Threading;

namespace Maze
{
    public static class MazePrinter
    {
        public static void Print(this MazeData maze)
        {
            for (var y = 0; y < maze.Data.GetLength(0); y++)
            for (var x = 0; x < maze.Data.GetLength(1); x++)
            {
                Console.SetCursorPosition(x, y);
                if (maze.Data[x, y] == 0)
                    Console.Write(" ");

                if (maze.Data[x, y] == 1)
                    Console.Write("▓");
                Thread.Sleep(1);
            }
        }

        public static void PrintPoint(this MazeData maze, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            if (maze.Data[x, y] == 0)
                Console.Write(" ");

            if (maze.Data[x, y] == 1)
                Console.Write("▓");

            Thread.Sleep(10);
        }
    }
}