# MazeGenerator

MazeGenerator is a C# project that generates mazes. It includes a console application demo to visualize the generated mazes.

## Features

- Supports different maze generation algorithms.
- Console application demo for maze visualization.
- Easy to extend and integrate with other applications.

## Getting Started

### Prerequisites

- .NET Core SDK

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/DimonSmart/MazeGenerator.git
    ```
2. Navigate to the project directory:
    ```bash
    cd MazeGenerator
    ```

### Running the Demo

1. Open the solution in Visual Studio or another IDE that supports .NET.
2. Set `MazeGeneratorConsoleDemo` as the startup project.
3. Run the project to see the maze generation in action.

## Usage

To use the maze generation library in your project, add a reference to the `MazeGenerator` project and use the provided APIs to generate mazes.

Example:
```csharp
var generator = new MazeGenerator();
var maze = generator.Generate(20, 20);
Console.WriteLine(maze);
