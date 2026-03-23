using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public class LinkedList<T>
        : IContainer<T>, IStaticSequence<T>, IDynamicSequence<T>, ISearchableSequence<T>, IEnumerable<T>
    {
        private Node<T>? _head;
        private int _size;


        /// <summary>
        /// Default constructor.
        /// Creates a new empty list. 
        /// </summary>
        public LinkedList()
        {
            _head = null;
            _size = 0;
        }

        /// <summary>
        /// Constructor with parameters.
        /// Creates a new list with the items in the IEnumerable object.
        /// </summary>
        /// <param name="items">A generic collection to copy in the list.</param>
        public LinkedList(IEnumerable<T> items)
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
            _size = 0;
        }

        public int GetSize()
        {
            return _size;
        }

        public bool IsEmpty()
        {
            return _size == 0;
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
        /// Inserts a new item at the given index.
        /// </summary>
        /// <param name="index">The index where the new item will be inserted.</param>
        /// <param name="item">The new item to insert.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the list range.</exception>
        public void InsertAt(int index, T item)
        {
            if (index == 0)
            {
                InsertFirst(item);
                return;
            }

            Node<T> previousNode = GetNodeAt(index - 1);
            var newNode = new Node<T>(item, previousNode.Next);
            previousNode.Next = newNode;
            _size++;
        }

        /// <summary>
        /// Removes the item at a given index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the list range.</exception>
        public void DeleteAt(int index)
        {
            if (index == 0)
            {
                DeleteFirst();
                return;
            }

            Node<T> previousNode = GetNodeAt(index - 1);

            // senza usare GetNodeAt due volte, che scorre la lista con complessità
            // O(n), prende solo quello precedente e usa quello per raggiungere
            // quello all'indice dato e quello puntato da esso.
            previousNode.Next = previousNode.Next!.Next;
            _size--;
        }

        /// <summary>
        /// Inserts a new item at the first position in the list.
        /// </summary>
        /// <param name="item">The new item to insert.</param>
        public void InsertFirst(T item)
        {
            _head = new Node<T>(item, _head);
            _size++;
        }

        /// <summary>
        /// Removes the first item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the list is empty.</exception>
        public void DeleteFirst()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot delete from an empty list.");


            _head = _head!.Next;
            _size--;
        }

        /// <summary>
        /// Inserts the new item in the last position.
        /// </summary>
        /// <param name="item">The new item to insert.</param>
        public void InsertLast(T item)
        {
            if (IsEmpty())
            {
                InsertFirst(item);
                return;
            }

            Node<T> lastNode = GetNodeAt(_size - 1);

            lastNode.Next = new Node<T>(item);
            _size++;
        }

        /// <summary>
        /// Removes the last item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the list is empty.</exception>
        public void DeleteLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot delete from an empty list.");

            if (_size == 1)
            {
                DeleteFirst();
                return;
            }

            Node<T> penultimateNode = GetNodeAt(_size - 2);
            penultimateNode.Next = null;
            _size--;
        }

        private Node<T> GetNodeAt(int index)
        {
            if (!ValidateIndex(index))
                throw new ArgumentOutOfRangeException("The index is out of range.");

            Node<T> currentNode = _head!;

            for (int i = 0; i < index; i++)
                currentNode = currentNode.Next!;

            return currentNode;
        }

        private bool ValidateIndex(int index)
        {
            return index >= 0 && index < _size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? currentNode = _head;

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
