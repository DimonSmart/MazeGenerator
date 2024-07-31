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
- Maze: Represents the maze structure with methods for wall creation and query.
- Cell: Basic unit in a maze implementing the ICell interface.
- MazeBuilder<T>: Generic builder class for constructing mazes with specific characteristics and algorithms.
- MazePathFinder: Static class providing pathfinding functionality within the maze using wave propagation.
## Usage
Add a reference to the MazeGenerator project in your application. Utilize the classes to generate and manipulate mazes:

```csharp
using MazeGenerator;

// Create a new maze with dimensions 31x21
var maze = new Maze(31, 21, () => new Cell(), new MazeConsolePlotter(TimeSpan.FromMilliseconds(25)));

// Build the maze with specific generation options
new MazeBuilder<Cell>(maze, new MazeGenerateOptions(0.50, 0.0)).Build();

// Find a path from start to a specified end point
var result = maze.FindPath(1, 1, MazePathFinder.GetEndPointCriteria(29, 19), wave => { wave.VisualizeWave(new MazeConsolePlotter()); });
if (result != null)
{
    result.VizualizePath(new MazeConsolePlotter());
}
```

### Contributions
Contributions are welcome! Please fork the repository and submit pull requests with your enhancements.