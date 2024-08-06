# MazeGenerator

MazeGenerator is a C# library designed to create complex mazes programmatically. This project also includes a console application demo that showcases how mazes are generated and visualized dynamically.

## Features

- **Dynamic Maze Generation**: Generate mazes with customizable dimensions and complexity.
- **Visualization**: Console application for visualizing maze generation and pathfinding processes.
- **Extensible Architecture**: Easily extendable to integrate different types of maze algorithms and visualization methods.
- **Pathfinding Support**: Includes pathfinding capabilities to find routes through the mazes.

## Getting Started

### Prerequisites

- .NET Core SDK (version specified or latest)

### Installation

Clone the repository and navigate to the project directory:

```bash
git clone https://github.com/DimonSmart/MazeGenerator.git
cd MazeGenerator
```

### Running the Demo
To see the MazeGenerator in action:

- Open the solution in Visual Studio or any compatible IDE.
- Set MazeGeneratorConsoleDemo as the startup project.
- Build and run the application to visualize the maze generation and pathfinding.
### Key Components

#### Classes and Interfaces

- `IMaze`: Interface defining the basic structure and functionality of a maze.
- `Maze`: Represents the maze structure with methods for wall creation and query.
- `Cell`: Basic unit in a maze implementing the ICell interface.
- `MazeBuilder<T>`: Generic builder class for constructing mazes with specific characteristics and algorithms.
- `MazePathFinder`: Static class providing pathfinding functionality within the maze using wave propagation.
- `IMazePlotter`: Interface for plotting elements like walls and passages within the maze, synchronously or asynchronously.

## Usage
Add a reference to the MazeGenerator project in your application. Utilize the classes to generate and manipulate mazes:

```csharp
using DimonSmart.MazeGenerator;
using DimonSmart.MazeGeneratorConsoleDemo;

// Create a new maze with dimensions 31x21 using the user-defined Cell class
var maze = new Maze<Cell>(31, 21);

// Initialize the maze plotter
var mazePlotter = new MazeConsolePlotter(TimeSpan.FromMilliseconds(25));

// Build the maze with specific generation options
new MazeBuilder(maze, new MazeBuildOptions(0.50, 0.0)).Build(mazePlotter);

// Find a path from start to a specified end point
var result = maze.FindPath(1, 1, GetEndPointCriteria(29, 19), wave => {
    wave.VisualizeWave(mazePlotter);
    Thread.Sleep(500);
});
result?.VisualizePath(mazePlotter);

// Keep the console open for 10 seconds
Console.CursorVisible = true;
Thread.Sleep(10000);

// Helper function to define endpoint criteria
private static Func<int, int, bool> GetEndPointCriteria(int endX, int endY)
{
    return (x, y) => x == endX && y == endY;
}

```

### Contributions
Contributions are welcome! Please fork the repository and submit pull requests with your enhancements.