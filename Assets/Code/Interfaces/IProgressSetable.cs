namespace Code.Interfaces
{
    public interface IProgressSetable<T>
    {
        void SetProgress(T progress);
    }
}