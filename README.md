# MazeGenerator
**Demo online:** [https://dimonsmart.github.io/Demo/](https://dimonsmart.github.io/Demo/)

MazeGenerator is a lightweight C# library for generating, visualizing, and solving mazes. The repo ships with a single CLI sample that walks through three small scenarios so you can verify the workflow without extra plumbing.

## Highlights
- **Deterministic generation** – control maze density via `MazeBuildOptions` (`WallShortness`, `Emptiness`).
- **Pathfinding out of the box** – `MazeWaveGenerator` + `MazePathBuilder` produces the shortest path.
- **Extensible plotters** – plug in your own `IMazePlotter`, `IWavePlotter`, or `IPathPlotter` to render progress in any UI.
- **Broad runtime support** – the library multi-targets `net6.0`, `net7.0`, `net8.0`, `net9.0`, and `net10.0`.

## Quick start

Run the text-mode sample (it plays all three scenarios back-to-back):

```bash
git clone https://github.com/DimonSmart/MazeGenerator.git
cd MazeGenerator
dotnet run --project QuickStartDemo --framework net8.0
```

You will see:
- **Example 1** – default 9×9 generation.
- **Example 2** – generation with `MazeBuildOptions` and a custom `IMazePlotter` implementation.
- **Example 3** – wave propagation + reconstructed path (shows `S`, `E`, and `*`).

The source lives in [QuickStartDemo/Program.cs](QuickStartDemo/Program.cs).

## Use the library in your app

1. **Install the package**

	```bash
	dotnet add package DimonSmart.MazeGenerator
	```

2. **Define a cell type** (implements `ICell`, has a public parameterless constructor):

	```csharp
	public sealed class TutorialCell : ICell
	{
		 private bool _isWall;
		 public bool IsWall() => _isWall;
		 public void MakeWall() => _isWall = true;
	}
	```

3. **Generate a maze, compute a wave, and reconstruct the path**:

	```csharp
	var maze = new Maze<TutorialCell>(31, 21);
	var options = new MazeBuildOptions(WallShortness: 0.15, Emptiness: 0.05);
	new MazeBuilder<TutorialCell>(maze, options).Build();

	var start = new Point(1, 1);
	var end = new Point(maze.Width - 2, maze.Height - 2);
	var wave = new MazeWaveGenerator<TutorialCell>(maze).GenerateWave(start.X, start.Y, end.X, end.Y);
	var path = new MazePathBuilder(wave).BuildPath();
	```

4. **Render however you like** – iterate over `maze` to print ASCII (see the QuickStart demo) or handle plotter callbacks:

	```csharp
	var plotter = new MyConsolePlotter();
	await new MazeBuilder<TutorialCell>(maze).BuildAsync(plotter);
	await new MazeWaveGenerator<TutorialCell>(maze, plotter).GenerateWaveAsync(start.X, start.Y, end.X, end.Y);
	await new MazePathBuilder(wave, plotter).BuildPathAsync();
	```

`MazeBuildOptions` cheatsheet:

- `WallShortness` – probability of interrupting long straight walls (higher value → twistier mazes).
- `Emptiness` – probability of skipping a wall entirely (higher value → more open space).

## Included demo

- **QuickStartDemo** – prints three compact scenarios (defaults, custom plotter, pathfinding). Run `dotnet run --project QuickStartDemo --framework net10.0` to validate the latest runtime.

## Key types

- `Maze<TCell>` – holds the grid and exposes boundary validation.
- `MazeBuilder<TCell>` – carves the maze; accepts optional `IMazePlotter` callbacks and cancellation tokens.
- `MazeWaveGenerator<TCell>` – propagates a Lee-style wave until it hits the target or exhausts the grid.
- `MazePathBuilder` – walks the wave back to produce a `MazePath` with ordered `PathCell` instances.
- Plotter interfaces (`IMazePlotter`, `IWavePlotter`, `IPathPlotter`) – override sync or async methods depending on how you want to render progress.

## Compatibility & testing

- Library multi-targets `net6.0; net7.0; net8.0; net9.0; net10.0`.
- QuickStartDemo targets `net6.0`, `net8.0`, and `net10.0` to ensure downstream apps compile cleanly across LTS and the latest SDK.
- `DimonSmart.MazeGenerator.Tests` (xUnit, `net8.0`) checks border generation plus wave/path reconstruction so regressions are caught automatically.
- `dotnet build MazeGenerator.sln` succeeds on .NET SDK 10.0.101 (checked on 2025-12-10).

## Contributing

Issues and pull requests are welcome. Please include a short description of the scenario you verified (QuickStart example number, custom UI, etc.) so regressions stay easy to triage.
