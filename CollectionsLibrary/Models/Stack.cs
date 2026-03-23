using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public partial class Stack<T>
        : IContainer<T>, IStack<T>
    {
        private Node<T>? _top;
        private int _size;

        /// <summary>
        /// Default constructor.
        /// Creates a new empty stack. 
        /// </summary>
        public Stack()
        {
            _top = null;
            _size = 0;
        }

        /// <summary>
        /// Constructor with parameters.
        /// Creates a new stack with the items in the IEnumerable object.
        /// </summary>
        /// <param name="items">A generic collection to copy in the stack.</param>
        public Stack(IEnumerable<T> items)
        {
            Build(items);
        }

        /// <summary>
        /// Overwrites the existing stack with the items of the IEnumerable object
        /// </summary>
        /// <param name="items">A generic collection to copy in the stack.</param>
        public void Build(IEnumerable<T> items)
        {
            /*
             * Clear() viene chiamato per primo per assicurarsi che Build() rimpiazzi sempre
             * tutti gli elementi presenti nello stack, per evitare di inserire gli elementi di IEnumerable
             * in cima a quelli già presenti.
             */

            Clear();

            foreach (T item in items)
                Push(item);
        }

        /// <summary>
        /// Clears the stack, emptying it.
        /// </summary>
        public void Clear()
        {
            _top = null;
            _size = 0;
        }

        /// <summary>
        /// Returns the number of items in the stack.
        /// </summary>
        /// <returns>Returns the size.</returns>
        public int GetSize()
        {
            return _size;
        }

        /// <summary>
        /// Returns whether the stack is empty or not.
        /// </summary>
        /// <returns>Returns true if it is empty or false if not.</returns>
        public bool IsEmpty()
        {
            return _size == 0;
        }

        /// <summary>
        /// Returns the item on the top of the stack without taking it out.
        /// </summary>
        /// <returns>The item on top.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the stack is empty</exception>
        public virtual T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The stack is empty.");

            return _top!.Item;
        }

        /// <summary>
        /// Returns the item on top of the stack and removes it.
        /// </summary>
        /// <returns>The item on top.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the stack is empty</exception>
        public virtual T Pop()
        {
            T item = Peek();

            _top = _top!.Next;
            _size--;

            return item;
        }

        /// <summary>
        /// Inserts a new item on top of the stack.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        public virtual void Push(T item)
        {
            Node<T>? currentTop = _top;
            _top = new Node<T>(item, currentTop);
            _size++;
        }
    }
}
