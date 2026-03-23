using System.Collections;
using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public partial class Queue<T>
        : IContainer<T>, IQueue<T>, IEnumerable<T>
    {
        private Node<T>? _front, _back;
        private int _length;

        public Queue()
        {
            _front = null;
            _back = null;
            _length = 0;
        }

        public Queue(IEnumerable<T> items)
        {
            Build(items);
        }

        public void Build(IEnumerable<T> items)
        {
            foreach (T item in items)
                Enqueue(item);
        }

        public void Clear()
        {
            _front = null;
            _back = null;
            _length = 0;
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The queue is empty.");

            Node<T> firstNode = _front!;
            _front = firstNode.Next;

            if (_front is null)
                _back = null;

            _length--;
            return firstNode.Item;
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (_front is null)
            {
                _front = newNode;
                _back = newNode;
            }
            else
            {
                _back!.Next = newNode;
                _back = newNode;
            }

            _length++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? currentNode = _front;

            while (currentNode is not null)
            {
                yield return currentNode.Item;
                currentNode = currentNode.Next;
            }
        }

        public int GetLength()
        {
            return _length;
        }

        public bool IsEmpty()
        {
            return _length == 0;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The queue is empty.");

            return _front!.Item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
