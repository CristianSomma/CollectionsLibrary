namespace CollectionsLibrary.Interfaces
{
    public interface IStack<T>
    {
        void Push(T item);
        T Peek();
        T Pop();
    }
}
