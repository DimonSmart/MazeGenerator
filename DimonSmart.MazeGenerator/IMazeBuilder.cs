namespace DimonSmart.MazeGenerator
{
    public interface IMazeBuilder
    {
        void Build(IMazePlotter? plotter = null, CancellationToken cancellationToken = default);
        Task BuildAsync(IMazePlotter? plotter = null, CancellationToken cancellationToken = default);
    }
}