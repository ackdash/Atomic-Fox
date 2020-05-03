namespace Code.Interfaces.Game
{
    public interface IPlayingCharacter
    {
        void Action();

        void Wait();

        void Alert();

        void Taunt();

        void Attacked();
        
    }
}