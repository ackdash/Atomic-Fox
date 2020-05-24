namespace Code.Interfaces
{
    public interface ICompletor
    {
        bool CheckState();
        bool TargetReached();
        void CheckIfComplete();
    }
}