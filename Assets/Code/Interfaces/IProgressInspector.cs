namespace Code.Interfaces
{
    public interface IProgressInspector
    {
        bool HasReachedTarget();
        float PercentComplete();
    }
}