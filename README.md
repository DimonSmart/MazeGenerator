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
- `MazePathFinder`: Class providing pathfinding functionality within the maze using wave propagation.
- `IMazePlotter`: Interface for plotting elements like walls and passages within the maze, synchronously or asynchronously.

## Usage
Add a reference to the MazeGenerator project in your application. Utilize the classes to generate and manipulate mazes:

```csharp
 var maze = new Maze<Cell>(31, 21);
 var mazePlotter = new MazeConsolePlotter();

 new MazeBuilder(maze, new MazeBuildOptions(0.50, 0.0)).Build(mazePlotter);

 var wave = new MazeWaveGenerator(maze, mazePlotter).GenerateWave(1, 1, 29, 19);
 var pathBuilder = new MazePathBuilder(wave, mazePlotter);
 pathBuilder.BuildPath();
```

### Contributions
Contributions are welcome! Please fork the repository and submit pull requests with your enhancements.
