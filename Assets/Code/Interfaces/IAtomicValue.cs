namespace Code.Interfaces
{
    public interface IAtomicValue<T>
    {
        T  Value
        {
            get;
            set;
        }
    }
}