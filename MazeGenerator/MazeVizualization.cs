﻿namespace MazeGenerator
{
    public static class MazeVizualization
    {
        public static Maze Redraw(this Maze maze)
        {
            Console.CursorVisible = false;
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    // Just for square look on console we draw square twice
                    Console.SetCursorPosition(x * 2, y);
                    var cellText = "  ";
                    if (maze.IsWall(x, y))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.White;
                        // On some monitors this character printed as not visible
                        // so we need a trick with foregraund and background colors
                        cellText = "▓▓";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Black;
                        cellText = "  ";
                    }
                    Console.WriteLine(cellText);
                }
            }
            return maze;
        }
    }
}