namespace CollectionsLibrary.Interfaces
{
    public interface IQueue<T>
    {
        T Peek();
        T Dequeue();
        void Enqueue(T item);
    }
}
