﻿
using MazeGenerator;

Console.Clear();
var maze = new Maze(11, 1, () => new Cell());
maze.Redraw();

new MazeBuilder<Cell>(maze, new MazeGenerateOptions(0.75))
    .Generate();
maze.Redraw();
