# MazeGenerator
**Demo online:** [https://dimonsmart.github.io/Demo/](https://dimonsmart.github.io/Demo/)

MazeGenerator is a C# library designed to create complex mazes programmatically. This project also includes a console application demo that showcases how mazes are generated and visualized dynamically.

## Features

- **Dynamic Maze Generation**: Generate mazes with customizable dimensions and complexity.
- **Visualization**: Console application for visualizing maze generation and pathfinding processes.
- **Extensible Architecture**: Easily extendable to integrate different types of maze algorithms and visualization methods.
- **Pathfinding Support**: Includes pathfinding capabilities to find routes through the mazes.

## Getting Started

### Prerequisites

- .NET 6 SDK or later

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
- `MazeBuilder<TCell>`: Generic builder class for constructing mazes with specific characteristics and algorithms.
- `MazeWaveGenerator<TCell>`: Generic wave generator class for pathfinding using wave propagation in the maze.
- `MazePathBuilder`: Builds a path using the generated wave data.
- `IMazePlotter`: Interface for plotting elements like walls and passages within the maze, synchronously or asynchronously.

## Usage
Add a reference to the MazeGenerator project in your application. Use these classes to generate and work with mazes:

### Example 1: Simple Maze Generation (No Visualization)
This example demonstrates how to generate a maze without any visualization.
The MazeBuilder is invoked without a plotter, so the maze is created silently.
```csharp
var maze = new Maze<Cell>(31, 21);
var random = new Random(0); // use a seed for repeatable mazes
new MazeBuilder<Cell>(maze, null, random).Build();
```

### Example 2: Maze Generation with Progress Visualization
In this example, the maze generation progress is visualized in the console.
A MazeConsolePlotter instance is passed to the MazeBuilder to display the building process.
```csharp
var maze = new Maze<Cell>(31, 21);
var mazePlotter = new MazeConsolePlotter();
var random = new Random(0); // seed for repeatable results
new MazeBuilder<Cell>(maze, new MazeBuildOptions(0.50, 0.0), random).Build(mazePlotter);
```

### Example 3: Wave Generation for Pathfinding
This example showcases the full process:
1. Maze generation with visualization.
2. Wave propagation for pathfinding from a start point to a target.
3. Path building based on the generated wave.
```csharp
var maze = new Maze<Cell>(31, 21);
var mazePlotter = new MazeConsolePlotter();
var random = new Random(0); // deterministic maze
new MazeBuilder<Cell>(maze, new MazeBuildOptions(0.50, 0.0), random).Build(mazePlotter);

var wave = new MazeWaveGenerator<Cell>(maze, mazePlotter).GenerateWave(1, 1, 29, 19);
var pathBuilder = new MazePathBuilder(wave, mazePlotter);
pathBuilder.BuildPath();
```

### Contributions
Contributions are welcome! Please fork the repository and submit pull requests with your enhancements.
