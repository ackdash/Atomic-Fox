namespace Code.Interfaces.Game
{
    public interface IActivatable
    {
        void Wait();
        
        void Activating();

        void Activate();

        void Deactivating();

        void Deactivated();

    }
}