using System.Collections;
using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public partial class DoublyLinkedList<T>
        : IContainer<T>, IStaticSequence<T>, IDynamicSequence<T>, ISearchableSequence<T>, IEnumerable<T>
    {
        private Node? _head, _tail;
        private int _size;

        /// <summary>
        /// Default constructor.
        /// Creates a new empty list. 
        /// </summary>
        public DoublyLinkedList()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        /// <summary>
        /// Constructor with parameters.
        /// Creates a new list with the items in the IEnumerable object.
        /// </summary>
        /// <param name="items">A generic collection to copy in the list.</param>
        public DoublyLinkedList(IEnumerable<T> items)
            : this()
        {
            Build(items);
        }

        /// <summary>
        /// Overwrites the existing list with the items of the IEnumerable object
        /// </summary>
        /// <param name="items">A generic collection to copy in the list.</param>
        public void Build(IEnumerable<T> items)
        {
            Clear();

            foreach (T item in items)
                InsertLast(item);
        }


        /// <summary>
        /// Clears the list, emptying it.
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        /// <summary>
        /// Returns the element at a given index.
        /// </summary>
        /// <param name="index">Index of the element to retrieve.</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the list range.</exception>
        public T GetAt(int index)
        {
            return GetNodeAt(index).Item;
        }

        /// <summary>
        /// Replaces the item at the given index with the new item.
        /// </summary>
        /// <param name="index">The index of the item to replace</param>
        /// <param name="newItem">The new item that will replace the one at the given index.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the list range.</exception>
        public void SetAt(int index, T newItem)
        {
            GetNodeAt(index).Item = newItem;
        }

        /// <summary>
        /// Inserts a new item at the given index.
        /// </summary>
        /// <param name="index">The index where the new item will be inserted.</param>
        /// <param name="item">The new item to insert.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the list range.</exception>
        public virtual void InsertAt(int index, T item)
        {
            Node currentNode = GetNodeAt(index);
            Node? previousNode = currentNode.Prev;

            Node newNode = new Node(
                item,
                previous: previousNode,
                next: currentNode);

            // Se il nodo precedente puntato da quello nella posizione dell'indice dato
            // non è null, allora avrà come nodo successivo quello nuovo, altrimenti
            // significa che il nodo nuovo è il primo nella lista, e sarà puntato da head.
            if (previousNode is not null)
                previousNode.Next = newNode;
            else
                _head = newNode;

            currentNode.Prev = newNode;
            _size++;
        }

        /// <summary>
        /// Removes the item at a given index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the list range.</exception>
        public virtual void DeleteAt(int index)
        {
            Node nodeToDelete = GetNodeAt(index);
            Node? previousNode = nodeToDelete.Prev;
            Node? nextNode = nodeToDelete.Next;

            if (previousNode is not null)
                previousNode.Next = nextNode;
            else
                _head = nextNode;

            if (nextNode is not null)
                nextNode.Prev = previousNode;
            else
                _tail = previousNode;

            _size--;
        }

        /// <summary>
        /// Inserts a new item at the first position in the list.
        /// </summary>
        /// <param name="item">The new item to insert.</param>
        public virtual void InsertFirst(T item)
        {
            Node newFirstNode = new Node(item, next: _head);

            if (!IsEmpty())
                _head!.Prev = newFirstNode;
            else
                _tail = newFirstNode;

            _head = newFirstNode;
            _size++;
        }

        /// <summary>
        /// Removes the first item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the list is empty.</exception>
        public virtual void DeleteFirst()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot delete from empty list.");

            _head = _head!.Next;

            if (_head is not null)
                _head.Prev = null;
            else
                _tail = null;

            _size--;
        }

        /// <summary>
        /// Inserts the new item in the last position.
        /// </summary>
        /// <param name="item">The new item to insert.</param>
        public virtual void InsertLast(T item)
        {
            Node newLastNode = new Node(item, previous: _tail);

            if (!IsEmpty())
                _tail!.Next = newLastNode;
            else
                _head = newLastNode;

            _tail = newLastNode;
            _size++;
        }

        /// <summary>
        /// Removes the last item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the list is empty.</exception>
        public virtual void DeleteLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot delete from empty list.");

            _tail = _tail!.Prev;

            if (_tail is not null)
                _tail.Next = null;
            else
                _head = null;

            _size--;
        }

        /// <summary>
        /// Returns whether the list is empty or not.
        /// </summary>
        /// <returns>Returns true if it is empty or false if not.</returns>
        public bool IsEmpty()
        {
            return _size == 0;
        }

        /// <summary>
        /// Returns the number of items in the list.
        /// </summary>
        /// <returns>Returns the size.</returns>
        public int GetSize()
        {
            return _size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node? currentNode = _head;

            while (currentNode is not null)
            {
                yield return currentNode.Item;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns the items of the list reversed.
        /// </summary>
        /// <returns>Item per item from the end of the list.</returns>
        public IEnumerable<T> Reverse()
        {
            Node? currentNode = _tail;

            while (currentNode is not null)
            {
                yield return currentNode.Item;
                currentNode = currentNode.Prev;
            }
        }

        private Node GetNodeAt(int index)
        {
            /*
             * -> Se l'indice dato è fuori dai limiti, lancia un'eccezione
             * 
             * -> In base a se l'indice è sotto la metà o sopra:
             *     - Se è sotto: Parte dalla testa e incrementa
             *     - Se è sopra: Parte dalla coda e decrementa
             *
             * ! -> Il doppio blocco for è preferibile perché su una grande mole
             *      di dati è più efficiente di un if che sceglie se andare al nodo
             *      puntato da Next o da Prev
            */

            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            Node? currentNode;

            if (index < _size / 2)
            {
                currentNode = _head;

                for (int i = 0; i < index; i++)
                    currentNode = currentNode!.Next;
            }
            else
            {
                currentNode = _tail;

                for (int i = _size - 1; i > index; i--)
                    currentNode = currentNode!.Prev;
            }

            return currentNode!;
        }

        /// <summary>
        /// Searches the index of the first item that is equal to the given item.
        /// </summary>
        /// <param name="item">Item to confront to.</param>
        /// <returns>The index of the item or -1 if no item is equal to the given one.</returns>
        public int FindIndex(T item)
        {
            return FindIndex(currentItem =>
            {
                // confronta ogni elemento corrente con quello del parametro usando 
                // l'uguaglianza di default del tipo T ('==' o '.Equals()')
                return EqualityComparer<T>.Default.Equals(currentItem, item);
            });
        }

        /// <summary>
        /// Returns the index of the first item that respects the predicate condition.
        /// </summary>
        /// <param name="predicate">The function containing the condition required.</param>
        /// <returns>The index or -1 if no item respects the condition.</returns>
        public int FindIndex(Predicate<T> predicate)
        {
            int index = 0;
            foreach (T currentItem in this)
            {
                if (predicate(currentItem))
                    return index;

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Returns whether the item searched is contained in the list or not.
        /// </summary>
        /// <param name="item">Item to search.</param>
        /// <returns>True if the item is found, otherwise false.</returns>
        public bool Contains(T item)
        {
            return FindIndex(item) != -1;
        }
    }
}
