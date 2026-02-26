using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public partial class Stack<T>
        : IContainer<T>, IStack<T>
    {
        private Node<T>? _top;
        private int _length;

        public Stack()
        {
            _top = null;
            _length = 0;
        }

        public Stack(IEnumerable<T> items)
        {
            Build(items);
        }

        public void Build(IEnumerable<T> items)
        {
            Node<T>? previousNode = null;

            foreach (T item in items)
            {
                Node<T> newNode = new Node<T>(item);

                _top ??= newNode;

                if (previousNode is not null)
                    previousNode.Next = newNode;

                previousNode = newNode;
                _length++;
            }
        }

        public void Clear()
        {
            _top = null;
            _length = 0;
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
                throw new InvalidOperationException("The stack is empty.");

            return _top!.Item;
        }

        public T Pop()
        {
            T item = Peek();

            _top = _top!.Next;
            _length--;

            return item;
        }

        public void Push(T item)
        {
            Node<T> currentTop = _top!;
            _top = new Node<T>(item, currentTop);
            _length++;
        }
    }
}
