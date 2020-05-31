namespace Code.Interfaces
{
    public interface ITeamProgressMonitorable : IProgressable, IProgressInspector, IProgressSetable<int>
    {
    }
}