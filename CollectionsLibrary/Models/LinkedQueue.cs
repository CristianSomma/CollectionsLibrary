using System.Collections;
using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public partial class LinkedQueue<T>
        : IContainer<T>, IQueue<T>
    {
        private Node<T>? _front, _back;
        private int _size;

        /// <summary>
        /// Default constructor.
        /// Creates a new empty queue.
        /// </summary>
        public LinkedQueue()
        {
            _front = null;
            _back = null;
            _size = 0;
        }

        /// <summary>
        /// Constructor with parameters.
        /// Creates a new queue with the items in the IEnumerable object.
        /// </summary>
        /// <param name="items">A generic collection to copy in the queue.</param>
        public LinkedQueue(IEnumerable<T> items)
        {
            Build(items);
        }

        /// <summary>
        /// Overwrites the existing queue with the items of the IEnumerable object
        /// </summary>
        /// <param name="items">A generic collection to copy in the queue.</param>
        public void Build(IEnumerable<T> items)
        {
            Clear();

            foreach (T item in items)
                Enqueue(item);
        }

        /// <summary>
        /// Clears the queue, emptying it.
        /// </summary>
        public void Clear()
        {
            _front = null;
            _back = null;
            _size = 0;
        }

        /// <summary>
        /// Returns the first element, in front, of the queue taking it out.
        /// </summary>
        /// <returns>Returns the first item</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public virtual T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The queue is empty.");

            Node<T> firstNode = _front!;
            _front = firstNode.Next;

            // Equivalente di IsEmpty() solo che qui _size non è ancora aggiornato
            // quindi avrei un risultato sbagliato.
            // Se la queue ha il fronte null è vuota, quindi si aggiorna anche il fondo.
            if (_front is null)
                _back = null;

            _size--;
            return firstNode.Item;
        }

        /// <summary>
        /// Inserts the new item at the back of the queue.
        /// </summary>
        /// <param name="item">The new item to insert.</param>
        public virtual void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (IsEmpty())
            {
                _front = newNode;
                _back = newNode;
            }
            else
            {
                _back!.Next = newNode;
                _back = newNode;
            }

            _size++;
        }

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        /// <returns>Returns the size.</returns>
        public int GetSize()
        {
            return _size;
        }

        /// <summary>
        /// Returns whether the queue is empty or not.
        /// </summary>
        /// <returns>Returns true if it is empty or false if not.</returns>
        public bool IsEmpty()
        {
            return _size == 0;
        }

        /// <summary>
        /// Returns the item in the front of the queue without taking it out.
        /// </summary>
        /// <returns>The item in front.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty</exception>
        public virtual T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The queue is empty.");

            return _front!.Item;
        }
    }
}
